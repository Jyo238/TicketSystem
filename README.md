# ticket-system

---

## 項目

ticket tracking system -  allows QA to report a bug and RD can mark a bug as resolved.

## 初始化專案

### 必須安裝的項目
https://dotnet.microsoft.com/en-us/download/dotnet/6.0 .net SDK 6.0

https://docs.microsoft.com/zh-tw/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
SQL EXPRESS 2019 (可選擇基本安裝，或者是[自訂媒體]=>[LocalDB套件]。)

### 各身分之帳號密碼
選擇強式密碼：使用八個或多個字元，以及至少一個大寫字元、數位和符號。 例如， Passw0rd! 符合強式密碼需求。

從專案的 資料夾中執行下列命令，其中 <PW> 是密碼：
```shell
dotnet user-secrets set SeedUserPW <PW>
```

### 開發環境執行

```shell
dotnet run
```

## 框架與主要程式庫

-.net Core MVC


## 結構

- /
  - Authorization - 驗證身分相關
  - Data - DbContext、SeedData
  - Enums - 就Enums
  - Models - Model
  - Pages - 頁面相關
  - Program.cs - setting Build & run
  - startUp.cs 設定應用程式要使用的服務和處理請求


## 專案位置

https://github.com/Jyo238/TicketSystem

```
https://github.com/Jyo238/TicketSystem.git
```

