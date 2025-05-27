# ERP Mobil API

ERP sistemine entegre çalışan, müşteri yönetimi, fatura oluşturma ve diğer ERP işlemlerini gerçekleştiren REST API projesi.

## Özellikler

### Müşteri Modülü
- **Müşteri Oluşturma:** Yeni müşteri kaydı oluşturma
- **Müşteri Güncelleme:** Mevcut müşteri bilgilerini güncelleme
- **Müşteri Listeleme:** Tüm müşterileri listeleme ve filtreleme
- **Müşteri Detayları:** Müşteri detay bilgilerini görüntüleme
- **Adres Yönetimi:** Müşteri adres bilgilerini ekleme, güncelleme ve silme
- **İletişim Bilgileri Yönetimi:** Müşteri iletişim bilgilerini (telefon, e-posta) ekleme, güncelleme ve silme
- **Kişi Yönetimi:** Müşteri ile ilişkili kişileri ekleme, güncelleme ve silme

### Fatura Modülü
- **Satış Faturası Oluşturma:** Yeni satış faturası oluşturma
- **Fatura Listeleme:** Faturaları listeleme ve filtreleme
- **Fatura Detayları:** Fatura detay bilgilerini görüntüleme

### Ürün Modülü
- **Ürün Listeleme:** Tüm ürünleri listeleme ve filtreleme
- **Ürün Detayları:** Ürün detay bilgilerini görüntüleme
- **Ürün Fiyat Listesi:** Satış fiyat listelerini görüntüleme ve filtreleme

### Diğer Özellikler
- **Para Birimi Yönetimi:** Para birimi bilgilerini listeleme
- **Ödeme Planı Yönetimi:** Ödeme planlarını listeleme ve yönetme

## Teknolojiler

- ASP.NET Core 7.0
- Dapper (Micro ORM)
- Microsoft SQL Server
- JWT Authentication
- Swagger API Documentation

## Son Güncellemeler

### v1.2.0 (27 Mayıs 2025)
- **Ürün Fiyat Listesi API'si:** Ürün fiyat listelerini görüntüleme ve filtreleme özelliği eklendi
  - Tarih aralığına göre filtreleme
  - Ürün kodu/açıklamasına göre arama
  - Sayfalama özelliği
  - Fiyat listesi başlık ve satır bilgilerini birleştirme

### v1.1.0 (30 Nisan 2025)
- **Müşteri Güncelleme API'si:** Trace ve SP uyumlu müşteri güncelleme API'si eklendi
  - Müşteri ana bilgilerini güncelleme
  - Adres bilgilerini ekleme, güncelleme ve silme
  - İletişim bilgilerini ekleme, güncelleme ve silme
  - Kişi bilgilerini ekleme, güncelleme ve silme
- **Adres ve İletişim Bilgileri İyileştirmeleri:**
  - Şehir koduna göre otomatik ülke ve eyalet kodu atama
  - İletişim tipi kodlarını dinamik olarak alma
  - E-posta güncellemelerinde EArchieveEMailCommunicationID güncelleme

### v1.0.0 (1 Nisan 2025)
- İlk sürüm
- Müşteri oluşturma ve listeleme
- Fatura oluşturma ve listeleme

## API Kullanımı

### Müşteri Oluşturma
```http
POST /api/v1/customer/create-new
```

### Müşteri Güncelleme
```http
POST /api/v1/customer/update
```

### Müşteri Listeleme
```http
GET /api/v1/customer
```

### Müşteri Detayları
```http
GET /api/v1/customer/{customerCode}
```

### Müşteri İletişim Bilgileri
```http
GET /api/v1/customer/{customerCode}/communications
```

### Müşteri Adres Bilgileri
```http
GET /api/v1/customer/{customerCode}/addresses
```

### Müşteri Kişi Bilgileri
```http
GET /api/v1/customer/{customerCode}/contacts
```

### Ürün Fiyat Listesi
```http
GET /api/v1/Product/all-price-lists
```

### Ürün Detayları
```http
GET /api/v1/Product/{productCode}
```

## Kurulum

1. Repoyu klonlayın:
```bash
git clone https://github.com/yilmazbor2024/erp-api.git
```

2. Proje dizinine gidin:
```bash
cd erp-api
```

3. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

4. Uygulamayı çalıştırın:
```bash
dotnet run
```

5. Tarayıcınızda Swagger dokümantasyonunu açın:
```
http://localhost:5180/swagger
```

## Lisans

Bu proje özel lisans altında dağıtılmaktadır. Tüm hakları saklıdır.
