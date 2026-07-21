# TicketBox 🎟️

TicketBox kullanıcıların etkinlikleri keşfedebildiği, filtreleyebildiği, bilet rezervasyonu oluşturabildiği ve yapay zeka destekli asistan ile kendine uygun etkinliği bulabildiği modern bir bilet satış platformudur.

Proje, sürdürülebilir ve test edilebilir bir yapı hedefiyle **Onion Architecture** yaklaşımıyla geliştirilmiştir. Arayüz, API, iş kuralları, veri erişimi ve dış servis entegrasyonları birbirinden ayrıştırılmıştır.

<p align="center">
  <img src="docs/demo.gif" alt="Home" />
</p>

## Öne Çıkan Özellikler

- Kullanıcı kayıt ve giriş işlemleri
- JWT tabanlı API kimlik doğrulaması
- Cookie tabanlı Web UI oturumu
- Etkinlik, kategori ve hero slider yönetimi
- Öne çıkarılmış etkinlikler
- Etkinlik adı, kategori ve fiyat aralığına göre gelişmiş arama/filtreleme
- Etkinlik rezervasyonu ve kontenjan kontrolü
- Her bilet için benzersiz PNR üretimi
- Bilet görseli oluşturma ve e-posta eki olarak gönderme
- Kullanıcı paneli, profil ve biletlerim ekranları
- Swagger üzerinden API dokümantasyonu
- SignalR ile gerçek zamanlı chatbot iletişimi
- Claude AI destekli, ruh haline göre etkinlik öneri sistemi
- Seçilen etkinlik için Tavily üzerinden güncel web bilgisi desteği

---

## TicketBox'ı Farklı Kılan Nedir?

TicketBox yalnızca etkinlik listeleyen bir bilet sistemi değildir. Kullanıcının etkinlik seçme sürecini kolaylaştırmayı hedefler.

Platformdaki yapay zeka asistanı:

- Kullanıcının belirttiği ruh haline göre öneri üretir.
- Önerilerini veritabanındaki gerçek etkinlik listesiyle sınırlar, sistemde olmayan etkinlikleri önermemesi için yönlendirilmiştir.
- Kullanıcı belirli bir etkinliği seçtiğinde, Tavily Search API ile o etkinlik hakkında güncel web bilgisi alabilir.
- Etkinlik öncesi kısa öneriler ve bağlama uygun bilgiler sunar.
- SignalR altyapısı sayesinde klasik sayfa yenileme gerektirmeden gerçek zamanlı sohbet deneyimi sağlar.

Bu yaklaşım, TicketBox'ı yalnızca “bilet satın alma” odaklı platformlardan ayırarak daha kişisel bir etkinlik keşif deneyimine dönüştürür.

---

## Kullanılan Teknolojiler

### Backend

- **.NET 8**
- **ASP.NET Core Web API**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **ASP.NET Core Identity**
- **JWT Bearer Authentication**
- **Cookie Authentication**
- **MediatR**
- **CQRS**
- **FluentValidation**
- **AutoMapper**
- **SignalR**
- **Swagger / OpenAPI**
- **MailKit**
- **SkiaSharp**
- **Newtonsoft.Json**

### Yapay Zeka ve Harici Servisler

- **Anthropic Claude API**  
  Sohbet asistanının kullanıcı mesajlarını yanıtlaması ve etkinlik önerileri üretmesi için kullanılır.

- **Tavily Search API**  
  Seçilen etkinlik hakkında güncel web sonuçları ve özet bilgi sağlamak için kullanılır.

- **SMTP / MailKit**  
  Oluşturulan bilet görsellerini kullanıcıya e-posta eki olarak göndermek için kullanılır.

### Frontend

- ASP.NET Core MVC / Razor Views
- JavaScript
- HTML5 / CSS3
- SignalR JavaScript Client
- Dinamik chatbot

---

## Mimari

Proje, **Onion Architecture** prensiplerine uygun olarak katmanlara ayrılmıştır.

```text
TicketBox
│
├── Core
│   ├── TicketBox.Domain
│   │   ├── Entities
│   │   └── Temel iş modeli
│   │
│   └── TicketBox.Application
│       ├── CQRS
│       ├── MediatR Commands / Queries / Handlers
│       ├── DTO / Result modelleri
│       ├── Repository ve Service interface'leri
│       ├── FluentValidation doğrulamaları
│       └── AutoMapper profilleri
│
├── Infrastructure
│   ├── TicketBox.Persistence
│   │   ├── Entity Framework Core
│   │   ├── SQL Server context
│   │   ├── Migrations
│   │   └── Repository implementasyonları
│   │
│   └── TicketBox.Infrastructure
│       ├── JWT servisi
│       ├── E-posta servisi
│       ├── Bilet görseli üretimi
│       ├── Claude AI entegrasyonu
│       └── Tavily Search entegrasyonu
│
└── Presentation
    ├── TicketBox.WebAPI
    │   ├── REST API
    │   ├── Swagger
    │   ├── SignalR ChatHub
    │   └── Exception middleware
    │
    └── TicketBox.WebUI
        ├── MVC kullanıcı arayüzü
        ├── Kullanıcı paneli
        ├── Etkinlik arama ekranı
        ├── Biletlerim ekranı
        └── Chatbot widget
```

### Katmanların Sorumlulukları

| Katman | Sorumluluk |
|---|---|
| `Domain` | Entity'ler ve temel iş kuralları |
| `Application` | Uygulama akışları, CQRS, MediatR handler'ları, interface'ler ve validasyonlar |
| `Persistence` | Entity Framework Core, SQL Server, repository implementasyonları |
| `Infrastructure` | E-posta, JWT, görsel üretimi, Claude ve Tavily gibi dış servis entegrasyonları |
| `WebAPI` | REST endpoint'leri, Swagger, SignalR hub ve middleware'ler |
| `WebUI` | Razor tabanlı kullanıcı arayüzü ve kullanıcı deneyimi |

Bu yapı sayesinde iş kuralları, veri erişimi ve dış servis bağımlılıkları birbirinden ayrılmıştır.

---

## Chatbot Akışı

```text
Kullanıcı
   │
   │ Mesaj + ruh hali + opsiyonel etkinlik seçimi
   ▼
WebUI Chat Widget
   │
   │ SignalR
   ▼
ChatHub
   │
   │ MediatR Command
   ▼
AskChatbotCommandHandler
   ├── Veritabanındaki güncel etkinlikleri alır
   ├── Seçili etkinlik varsa Tavily ile güncel bilgi arar
   ├── Etkinlik listesi, ruh hali ve güncel bilgiyi prompt'a ekler
   ▼
Claude API
   │
   ▼
SignalR üzerinden kullanıcıya gerçek zamanlı yanıt
```

Chatbot, etkinlik önerilerini veritabanında kayıtlı olan etkinliklere göre oluşturur. Böylece kullanıcıya platformda olmayan biletler önerilmesinin önüne geçilmesi hedeflenir.

---

## Rezervasyon ve Bilet Süreci

1. Kullanıcı bir etkinlik ve adet seçerek rezervasyon oluşturur.
2. Sistem etkinliğin mevcut kontenjanını kontrol eder.
3. Rezervasyon kaydı oluşturulur ve kontenjan güncellenir.
4. Her bilet için benzersiz bir PNR üretilir.
5. SkiaSharp ile bilet görseli hazırlanır.
6. Bilet görseli `wwwroot/tickets` altında saklanır.
7. Bilet, SMTP üzerinden kullanıcı e-posta adresine ek olarak gönderilir.

---

## Proje Yapısı

```text
TicketBox.sln
├── Core
│   ├── TicketBox.Domain
│   └── TicketBox.Application
├── Infrastructure
│   ├── TicketBox.Persistence
│   └── TicketBox.Infrastructure
└── Presentation
    ├── TicketBox.WebAPI
    └── TicketBox.WebUI
```

---

Bu proje eğitim amaçlı geliştirilmiştir.
