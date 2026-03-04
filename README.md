# Foreign Fund Manager（外国籍投資信託管理システム）

資産管理銀行における外国籍投資信託の管理業務を支援するデスクトップアプリケーション。海外ファンド管理会社から受領する基準価額（NAV）データの取込、為替換算、ファンド情報の管理、および月次運用レポートの出力を行う。

## デモ画像
<img width="1000" height="749" alt="スクリーンショット 2026-03-04 085157" src="https://github.com/user-attachments/assets/4b17c0e9-d950-4c05-a1d5-7ba17289b10e" />
<img width="996" height="753" alt="スクリーンショット 2026-03-04 085219" src="https://github.com/user-attachments/assets/4681926a-ef37-4b96-bcd1-709ed1eed4f8" />
<img width="1153" height="657" alt="スクリーンショット 2026-02-14 205700" src="https://github.com/user-attachments/assets/b4153875-1ecd-4aad-b240-126a8cc4412b" />


## 技術スタック

| 項目 | 技術 |
|------|------|
| 言語 | VB.NET (.NET Framework 4.8) |
| UI | Windows Forms (MDI) |
| バッチ | コンソールアプリケーション |
| データアクセス | ADO.NET（パラメタライズドクエリ） |
| データベース | SQL Server Express 2019+ |
| Excel出力 | EPPlus 4.5.3.3 |
| ログ | NLog 5.x |
| ビルド | MSBuild / Visual Studio 2022 |

## ソリューション構成

```
ForeignFundManager.sln
├── ForeignFundManager.UI       … WinForms画面（10画面）
├── ForeignFundManager.Batch    … コンソールバッチ（JPY換算）
├── ForeignFundManager.Core     … ビジネスロジック（サービス・バリデータ・ユーティリティ）
└── ForeignFundManager.Data     … データアクセス（モデル・リポジトリ）
```

依存方向: `UI/Batch → Core → Data → SQL Server`（単方向）

## 主要機能

- **ファンドマスタ管理** — 登録・編集・論理削除・変更履歴・CSV出力
- **為替レート管理** — TTM/TTS/TTB 登録・検索
- **営業日カレンダー** — 祝日の追加・削除
- **データ取込（CSV）** — NAV・為替レート・カレンダーの一括インポート（全件検証後に一括登録）
- **JPY換算バッチ** — 外貨建てNAV × TTMレートで円建てNAV算出（前営業日遡及ロジック付き）
- **月次レポート出力** — Excel形式（NAV推移チャート・純資産推移・リターンテーブル・為替レート）

## セットアップ

### 前提条件

- Visual Studio 2022（VB.NETワークロード）
- SQL Server Express 2019+
- .NET Framework 4.8

### 手順

1. **データベース構築**
   - SQL Server に `ForeignFundManager` データベースを作成
   - `sql/` フォルダのスクリプトを順番に実行

2. **NuGetパッケージ復元**
   ```
   nuget restore ForeignFundManager.slnx
   ```

3. **ビルド**
   ```
   msbuild ForeignFundManager.slnx /p:Configuration=Debug
   ```

4. **接続文字列の確認**
   - `ForeignFundManager.UI/App.config` と `ForeignFundManager.Batch/App.config` の接続先が環境に合っているか確認

5. **実行**
   - UI: `ForeignFundManager.UI/bin/Debug/ForeignFundManager.UI.exe`
   - Batch: `ForeignFundManager.Batch/bin/Debug/ForeignFundManager.Batch.exe /date:2025-01-15`

## バッチ引数

```
ForeignFundManager.Batch.exe /date:YYYY-MM-DD
```

| 終了コード | 意味 |
|------------|------|
| 0 | 正常終了 |
| 1 | 一部エラーあり |
| 9 | 異常終了 |

## 設計書

`docs/` フォルダに詳細設計書（日本語）を格納。

| ファイル | 内容 |
|----------|------|
| `01_要件定義書.md` | 要件定義 |
| `02_01_DB論理設計.md` | DB論理設計 |
| `02_02_システム構成設計.md` | アーキテクチャ・命名規則 |
| `02_03_画面設計.md` | 画面仕様（10画面） |
| `02_04_バッチ設計.md` | バッチ処理フロー |
| `02_05_帳票設計.md` | Excelレポート構成 |
| `02_06_外部インターフェース設計.md` | CSV入出力フォーマット |
| `03_01_DB物理設計.md` | DDL・インデックス |
| `03_02_クラス設計.md` | クラス詳細設計 |
