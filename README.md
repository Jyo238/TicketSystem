# ticket-system

---

## 項目

Ticket Tracking System -  allows QA to report a bug and RD can mark a bug as resolved.

## 初始化專案

### 必須安裝的項目
https://dotnet.microsoft.com/en-us/download/dotnet/6.0 .net SDK 6.0

https://docs.microsoft.com/zh-tw/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
SQL EXPRESS 2019

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
  - StartUp.cs 設定應用程式要使用的服務和處理請求


## 專案位置

https://github.com/Jyo238/TicketSystem

```
https://github.com/Jyo238/TicketSystem.git
```
  
---
  Task 1 - Please write down all the use cases either in text or diagram you can think for Phase I and Phase II requirement separately.
  
  https://button-weeder-9f4.notion.site/TicketSystem-faef89b133bf49aba4f4a83e22d73a98
  
  Task 3 - Think of yourself as an architect. How will you design this system, please write down the design document at least to include data model, class diagram and  UI mock up.
  
  https://www.figma.com/proto/sUQlNh7K7ElYETW2oChEvk/Untitled?node-id=2%3A2&starting-point-node-id=2%3A2

Task 4 - If we are going to open the system for 3rd party to use, can you please design the Web API(Json format) and api document?
  
https://button-weeder-9f4.notion.site/API-Document-3205599c4d5942988d526988dd4d0ea9
