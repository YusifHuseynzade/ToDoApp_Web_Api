# ToDoApp_Web_Api

Bu proje, .NET Core 6 ve N-Tier mimarisi kullanılarak geliştirilmiştir.

## Funksiyanallıqlar

- **İstifadəçi İdarəetməsi**
  - İstifadəçi qeydiyyatı (Register)
  - İstifadəçi girişi (Login) - JWT token və refresh token istifadəsi
  - İstifadəçiləri görüntüləmə
  - İstifadəçiləri silmə
  - İstifadəçi məlumatlarını dəyişmə

- **Yetkiləndirmə**
  - İstifadəçilərin giriş etdikdən sonra yalnızca yetkilə işləmləri icra etməsi
  - Yetkilərə görə iki əsas bölmə: Layihə rəhbəri və İşçi

- **Sprint Əməliyyatları**
  - Sprint əlavə etmək, dəyişmək, silmək

- **Tapşırıq Əməliyyatları**
  - Tapşırıq əlavə etmək, dəyişmək, silmək

- **Rollar**
  - İstifadəçilərin iki əsas rolu mövcuddur:
    - Layihə rəhbəri
    - İşçi

- **Layihə Rəhbəri Yetkiləri:**
  - Sprintlərin və tapşırıqların bütün proseslərinə nəzarət etmə
  - Tapşırıqlara işçi əlavə etmək, silmək və ya bu işçilərin bir tapşırıqdan başqa bir tapşırıqa yerini dəyişmək
  - Tapşırığın statusunu dəyişmək
  - Bütün sprintləri və ona aid olan tapşırıqları izləmək
  - Tapşırığı yeniləyərək ona aid olduğu sprinti dəyişmək
  - Todo List'də Code Review mərhələsində görüş yazmaq

- **İşçi Yetkiləri:**
  - Bütün Tapşırıqları görmək
  - Yalnız öz tapşırıqlarını görmək
  - Tapşırığın statusunu dəyişmək
  - Todo List'də Code Review mərhələsində yalnız assign edildiyi tapşırıq ilə əlaqəli rəy yazmaq
   
- Tapşırıqların expire tarixi bitdiyində, avtomatik olaraq statuslarını "failed" olaraq dəyişmə (Cron job ilə)

## Lisenziya
Bu layihə MIT Lisenziyası ilə lisenziyalanmışdır - detallar üçün [LICENSE.md](LICENSE.md) faylına baxın.
