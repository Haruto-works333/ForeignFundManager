Imports ForeignFundManager.Core.Constants
Imports ForeignFundManager.Core.Services
Imports NLog

Module Module1

    Private ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

    Sub Main()
        Dim exitCode = AppConstants.ExitCodeSuccess

        Try
            _logger.Info("=== NAV Calculation Batch Started ===")

            Dim targetDate = Date.Today
            Dim args = Environment.GetCommandLineArgs()

            For i = 1 To args.Length - 1
                Dim arg = args(i)
                If arg.StartsWith("/date:", StringComparison.OrdinalIgnoreCase) Then
                    Dim dateStr = arg.Substring(6)
                    If Not Date.TryParse(dateStr, targetDate) Then
                        Console.Error.WriteLine($"Invalid date format: {dateStr}")
                        Console.Error.WriteLine("Usage: ForeignFundManager.Batch.exe [/date:YYYY-MM-DD]")
                        Environment.ExitCode = AppConstants.ExitCodeFatal
                        Return
                    End If
                End If
            Next

            _logger.Info($"Target date: {targetDate:yyyy-MM-dd}")
            Console.WriteLine($"Target date: {targetDate:yyyy-MM-dd}")

            Dim service As New NavCalculationService()
            Dim result = service.Execute(targetDate)

            Console.WriteLine()
            Console.WriteLine("=== Batch Result ===")
            Console.WriteLine($"  Total:   {result.TotalCount}")
            Console.WriteLine($"  Success: {result.SuccessCount}")
            Console.WriteLine($"  Skipped: {result.SkippedCount}")
            Console.WriteLine($"  Error:   {result.ErrorCount}")
            Console.WriteLine($"  Elapsed: {result.Elapsed}")
            Console.WriteLine()

            For Each detail In result.Details
                Dim rateStr = If(detail.AppliedRate.HasValue, detail.AppliedRate.Value.ToString("F6"), "N/A")
                Dim jpyStr = If(detail.NavJpy.HasValue, detail.NavJpy.Value.ToString("N0"), "N/A")
                Console.WriteLine($"  {detail.Isin}  {detail.CurrencyCode}  NAV={detail.NavPerUnit:F6}  Rate={rateStr}  JPY={jpyStr}  [{detail.Status}] {detail.Note}")
            Next

            If result.ErrorCount > 0 OrElse result.SkippedCount > 0 Then
                exitCode = AppConstants.ExitCodeWarning
            End If

            _logger.Info($"=== NAV Calculation Batch Completed (ExitCode={exitCode}) ===")

        Catch ex As Exception
            _logger.Fatal(ex, "Batch terminated with fatal error")
            Console.Error.WriteLine($"FATAL: {ex.Message}")
            exitCode = AppConstants.ExitCodeFatal
        End Try

        Environment.ExitCode = exitCode
    End Sub

End Module
