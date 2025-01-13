3D Meyve Eşleştirme Oyunu
Bu proje, kullanıcıların meyveleri eşleştirerek puan kazandığı bir 3D oyun deneyimi sunar. Kullanıcılar, fare ile meyveleri sürükleyip bırakabilir, belirli kombinasyonları eşleştirerek puan kazanabilirler. Ayrıca bonus ve zaman durdurma gibi özel özellikler de mevcuttur.

Özellikler
•	Meyve Eşleştirme: Oyuncular, doğru meyve çiftlerini eşleştirerek puan kazanır. Yanlış eşleştirilen meyveler fırlatılır.
•	Mıknatıs Mekaniği: Seçilen meyveler belirli bir noktaya mıknatıs kuvveti ile çekilir.
•	Bonus Sistemi: Eşleşmelerde ekstra puan kazanmak için bonus özelliği etkinleştirilebilir.
•	Zaman Duraklatma: Oyuncular, belirli bir süre için zamanlayıcıyı duraklatabilir.
•	Patlama Efekti: Başarılı eşleşmelerde patlama efekti gösterilir.
•	Sürükle-Bırak Mekaniği: Meyveler, fare ile sürüklenip bırakılabilir.

Oyun Başlatma
•	Oyun, Start() fonksiyonu ile başlar. Oyun başladığında, sahnede meyveler belirli noktalara yerleştirilir.
•	Her meyve bir tag ile etiketlenmiştir. İki meyve eşleştirildiğinde, bu tagler karşılaştırılır.

Oynanış
•	Meyveleri fare ile seçip sürükleyerek oyun alanına yerleştirin.
•	Eşleşen meyveler patlar ve oyuncuya puan kazandırılır. Eşleşmeyenler ise fırlatılır.
•	Bonus ve zaman duraklatma butonları, ek özellikler sunar.

GUI ve Butonlar
•	Time: Kalan süreyi gösterir.
•	Score: Oyuncunun puanını gösterir.
•	RESET: Oyunu sıfırlayarak yeniden başlatır.
•	BONUS: Bonus özelliğini aktif eder, eşleşmelerde ekstra puan sağlar.
•	PAUSE TIMER: Zamanlayıcıyı duraklatır.

Bonus ve Zaman Duraklatma
•	Bonus: BONUS butonuna tıklayarak bonus puan kazanabilirsiniz. Bonus etkinleştirildiğinde, bir sonraki eşleşme 300 puan kazandırır.
•	Zaman Duraklatma: PAUSE TIMER butonuna tıklayarak zamanlayıcıyı 10 saniye boyunca duraklatabilirsiniz.

Kod Açıklamaları
matchMaker.cs
•	selectedFruit1 ve selectedFruit2: Seçilen iki meyve.
•	magnetStrength: Mıknatısın kuvveti.
•	EvaluateMatch(): İki meyve eşleştirildiğinde çağrılır ve eşleşme kontrolü yapar.
•	AttractToPoint(): Mıknatıs kuvvetini meyveye uygular.
•	EndGame(): Oyun sona erdiğinde çağrılır ve final skoru görüntüler.
•	InitializeFruits(): Başlangıçta meyvelerin rastgele yerleştirilmesini sağlar.

fruit.cs
•	OnMouseDown(): Meyveye tıklandığında, sürükleme işlemi başlatılır.
•	OnMouseDrag(): Fareyi hareket ettirirken, meyve hareket ettirilir.
•	OnMouseUp(): Meyve bırakıldığında, meyve yere düşer ve etkileşim sona erer.

Video
Oyunla ilgili oynanış videosunu aşağıdaki bağlantıdan izleyebilirsiniz:
https://youtu.be/BtDgMrQM6Iw

Webgl linki:
https://play.unity.com/en/games/cb77909b-65a5-4ecb-b98b-cf67ee79faea/match3d-oyunu

Yusuf BAŞÇI
200601029
