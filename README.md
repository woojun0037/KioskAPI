# 📖 Project Overview
ASP.NET Core Web API와 Entity Framework Core를 사용하여  
**상품 CRUD 기능을 제공하는 백엔드 서버를 구현한 프로젝트입니다.**

Controller-Service 구조와 DTO 분리를 적용하여  
유지보수성과 확장성을 고려한 설계를 목표로 했습니다.

---

## 🛠 Tech Stack
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- REST API

---

## 🏗 Architecture
Client (WPF 예정)
↓
Controller
↓
Service
↓
DbContext (EF Core)
↓
SQLite

---

## 📌 Key Features

### ✔ CRUD API
- 상품 생성 (POST)
- 상품 조회 (GET)
- 상품 수정 (PUT)
- 상품 삭제 (DELETE)

---

### ✔ Layered Architecture
- Controller: 요청/응답 처리
- Service: 비즈니스 로직 처리
- DbContext: 데이터 접근

👉 역할 분리를 통해 코드 구조 개선

---

### ✔ DTO Separation
- CreateProductRequest
- UpdateProductRequest
- ProductResponse

👉 Entity와 분리하여  
보안성과 유지보수성 향상

---

### ✔ Dependency Injection
```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```
👉 인터페이스 기반 설계 적용

### ✔ Async Processing
```csharp
await _context.Products.ToListAsync();
```
👉 비동기 처리로 성능 개선

## 📌 API Example

### ✔ GET /api/products
상품 목록 조회

#### Response
```json
[
  {
    "id": 1,
    "name": "아메리카노",
    "price": 1800
  },
  {
    "id": 2,
    "name": "카페라떼",
    "price": 2500
  }
]
```
### ✔ GET /api/products/{id}
상품 단건 조회

#### Response
```json
[
  {
    "id": 1,
    "name": "아메리카노",
    "price": 1800
  }
]
```
### ✔ POST /api/products
상품 생성

#### Request
```json
[
  {
    "name": "바닐라라떼",
    "price": 3500
  }
]
```
#### Response
```json
[
  {
  "id": 3,
  "name": "바닐라라떼",
  "price": 3500
  }
]
```
### ✔ PUT /api/products
상품 수정

#### Request
```json
[
  {
    "name": "수정된 카페라떼",
    "price": 2800
  }
]
```
#### Response
```json
[
  {
    "id": 2,
    "name": "수정된 카페라떼",
    "price": 2800
  }
]
```

###✔ DELETE /api/products/{id}
상품 삭제

#### Response
```json
 204 No Content
```

📌 What I Learned
RESTful API 설계 및 CRUD 구현
Controller-Service 계층 분리 설계
DTO를 활용한 데이터 보호 및 구조 개선
Entity Framework Core를 통한 DB 연동
async/await 기반 비동기 처리 이해

📌 Future Improvements
WPF 클라이언트 연동
글로벌 예외 처리
공통 응답 포맷 적용
AutoMapper 도입
인증/인가 기능 추가

📌 Summary
ASP.NET Core 기반의 CRUD API 서버를 구축하며,
계층 분리와 DTO 설계를 통해 실무에 가까운 백엔드 구조를 경험한 프로젝트입니다.
