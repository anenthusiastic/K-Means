using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace K_means
{
    class Program
    {
        static Random random = new Random();


        static void Main(string[] args)
        {
            sosyalMedyaKullanımı(3);
            

            Console.ReadLine();
        }
        static void zambakTürleri(int türsayısı) //zambak veriseti için k-means algoritmasını gerçekleştiren method
        {
            Veri[] merkezler = İlkKumeMerkezleri(türsayısı, 4);
            Veri[] çiçekler = new Veri[150];

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\fatih\source\repos\K-means\K-means\Veriseti.txt");
            for (int i = 0; i < lines.Length/2; i++)
            {
                string[] veriler = lines[2*i].Split(',');
                çiçekler[i] = new Veri(double.Parse(veriler[0], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler[1], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler[2], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler[3], System.Globalization.CultureInfo.InvariantCulture));
                string[] veriler2 = lines[2 * i + 1].Split(',');
                çiçekler[i + 75] = new Veri(double.Parse(veriler2[0], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler2[1], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler2[2], System.Globalization.CultureInfo.InvariantCulture), double.Parse(veriler2[3], System.Globalization.CultureInfo.InvariantCulture));
            }


            ArrayList[] kumelenmisler = kümele(çiçekler, merkezler);
            Veri[] yenimerkezler = YeniKumeMerkezleri(kumelenmisler, merkezler);
            Console.WriteLine("Devam etmek için enter'a basınız...");
            Console.ReadLine();
            while (!merkezlerEsitmi(merkezler, yenimerkezler))
            {
                merkezler = yenimerkezler;
                kumelenmisler = kümele(çiçekler, merkezler);
                yenimerkezler = YeniKumeMerkezleri(kumelenmisler, merkezler);
                Console.WriteLine("Devam etmek için enter'a basınız...");
                Console.ReadLine();
            }
            

            Console.WriteLine("Programı kapatmak için enter'a basınız.");
        }
        static void sosyalMedyaKullanımı(int merkezsayısı) // sosyal medya kullnımı veriseti için k-means algoritmasını
                                                            //gerçekleştiren method.
        {
            Veri[] merkezler = İlkKumeMerkezleri(merkezsayısı,3);
            Veri[] kişiler = new Veri[10];
            kişiler[0] = new Veri(90, 3, 5);
            kişiler[1] = new Veri(53, 37, 35);
            kişiler[2] = new Veri(48, 52, 40);
            kişiler[3] = new Veri(85, 7, 2);
            kişiler[4] = new Veri(5, 55, 4);
            kişiler[5] = new Veri(93, 8, 25);
            kişiler[6] = new Veri(11, 75, 6);
            kişiler[7] = new Veri(8, 9, 80);
            kişiler[8] = new Veri(5, 19, 80);
            kişiler[9] = new Veri(9, 3, 91);

            ArrayList[] kumelenmisler = kümele(kişiler, merkezler);
            Veri[] yenimerkezler = YeniKumeMerkezleri(kumelenmisler, merkezler);
            Console.WriteLine("Devam etmek için enter'a basınız...");
            Console.ReadLine();
            while (!merkezlerEsitmi(merkezler, yenimerkezler))
            {
                merkezler = yenimerkezler;
                kumelenmisler = kümele(kişiler, merkezler);
                yenimerkezler = YeniKumeMerkezleri(kumelenmisler, merkezler);
                Console.WriteLine("Devam etmek için enter'a basınız...");
                Console.ReadLine();
            }
            yenikişi(yenimerkezler);
            Console.WriteLine();
            Console.WriteLine("İşlem bitti.Programı kapatmak için entera basınız.");
        }
        static ArrayList [] kümele(Veri[] veriler, Veri[] kumeler) // aldığı veri dizisindeki her bir elemanın,aldığı 
                                                                    // kumeler veri dizisindeki hangi kümeye ait olduğunu
                                                                    // bulan ve geriye ait olduğu kümelere göre arraylist-
                                                                    //lere atılmıi verileri döndüren method.kısaca kümeleme
                                                                    //yapıyor.
        {
            ArrayList [] kumelenmisVeriler = new ArrayList[kumeler.Length];

            Console.WriteLine("\tÖznitelikler\t  Min Uzaklık\tKüme No\tKüme Merkezi");

            for (int i = 0; i < veriler.Length; i++)
            {
                double toplam = 175;
                
                int kumeNo=0;
                for (int j = 0; j < kumeler.Length; j++)
                {
                    double gecicitoplam = 0;
                    if (i == 0)
                    {
                        kumelenmisVeriler[j] = new ArrayList();
                    }
                    
                    gecicitoplam += Math.Pow(veriler[i].X - kumeler[j].X, 2);
                    gecicitoplam += Math.Pow(veriler[i].Y - kumeler[j].Y, 2);
                    gecicitoplam += Math.Pow(veriler[i].Z - kumeler[j].Z, 2);
                    gecicitoplam += Math.Pow(veriler[i].S - kumeler[j].S, 2);
                    gecicitoplam = Math.Sqrt(gecicitoplam);
                    if (gecicitoplam < toplam)
                    {
                        toplam = gecicitoplam;
                        
                        kumeNo = j+1;
                    }
                    
                }
                
                kumelenmisVeriler[kumeNo-1].Add(veriler[i]);

                Console.Write(i + 1 + "\t");
                Console.Write(veriler[i].ToString() + "\t");
                Console.Write(string.Format("  " +"{0:F6}",toplam) + " \t  ");
                Console.Write(kumeNo + "\t");
                Console.Write(kumeler[kumeNo - 1].ToString());
                Console.WriteLine();
                
                
            }
            Console.WriteLine();
            return kumelenmisVeriler;
        }
        
        
        static Veri[] YeniKumeMerkezleri(ArrayList[] kumelenmisler,Veri[] merkezler)//kumelenmis verileri alıp bu verilere göre
                                                                                    //herbir küme merkezini tekrardan belirleyen
                                                                                    //ve bu kumemerkezlerini veri dizisi şeklinde
                                                                                    //geri döndüren method
        {
            Veri[] yenimerkezler = new Veri[kumelenmisler.Length];
            
            for(int i= 0; i < kumelenmisler.Length; i++)
            {
                double x = 0; double y = 0; double z = 0; double s = 0;

                ArrayList icerdekidizi = kumelenmisler[i];
                if(icerdekidizi.Count == 0)
                {
                    yenimerkezler[i] = new Veri(merkezler[i].X, merkezler[i].Y, merkezler[i].Z,merkezler[i].S);
                }
                else
                {
                    for(int j = 0; j < icerdekidizi.Count; j++)
                    {
                        x += ((Veri)icerdekidizi[j]).X;
                        y += ((Veri)icerdekidizi[j]).Y;
                        z += ((Veri)icerdekidizi[j]).Z;
                        s += ((Veri)icerdekidizi[j]).S;
                    }
                    yenimerkezler[i] = new Veri(x / icerdekidizi.Count, y / icerdekidizi.Count, z / icerdekidizi.Count,s / icerdekidizi.Count);
                }
                
            }
            return yenimerkezler;
        }
        
        static Veri[] İlkKumeMerkezleri(int merkezsayısı,int özniteliksayısı)   //istenen bir sayıdaki ve öznitelikteki
                                                                                // ilk küme merkezlerini random şekilde
                                                                               // belirleyen metod
        {
            Veri[] merkezler = new Veri[merkezsayısı];
            for (int i = 0; i < merkezsayısı; i++)
            {
                if(özniteliksayısı == 3) // sosyal medya kullanımı veriseti için küme merkezlerini belirleyen kod
                {
                    int x = random.Next(100);
                    int y = random.Next(100);
                    int z = random.Next(100);
                    Veri merkez = new Veri(x, y, z);
                    merkezler[i] = merkez;
                }
               if(özniteliksayısı == 4) // çiçek veriseti için küme merkezlerini belirleyen kod
                {
                    double x = random.Next(43,78)/10.0;
                    double y = random.Next(20,45)/10.0;
                    double z = random.Next(10,70)/10.0;
                    double s = random.Next(1,26)/10.0;
                    Veri merkez = new Veri(x, y, z, s);
                    merkezler[i] = merkez;
                }
                
            }
            return merkezler;
        }
        static bool merkezlerEsitmi(Veri[] eski,Veri[] yeni) // programın bitmesi içiçn gerekli koşul olan eski ve yeni
                                                              // küme merkezlerinin aynı olması koşulunu kontrol
                                                              //etmek için gerekli method
        {
            for(int i = 0; i < eski.Length; i++)
            {
                if(!eski[i].Equals(yeni[i]))
                {
                    return false;
                }
                
            }
            return true;
        }
        static void yenikişi(Veri [] merkezler)//sosyal medya verisetinin son k-means algoritmasına göre son hali 
                                               // belirlendikten sonra bilgileri verilen kişiyi kümeleyen method.
        {
            Console.WriteLine("X koordinatı :");
            int x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Y koordinatı :");
            int y =Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Z koordinatı :");
            int z = Convert.ToInt16(Console.ReadLine());
            Veri yeniveri = new Veri(x, y, z);

            double uzaklık = 175;
            int kume = 0;
            for (int i = 0; i < merkezler.Length; i++)
            {
                double gecici = 0;
                
                gecici += Math.Pow(yeniveri.X - merkezler[i].X, 2);
                gecici += Math.Pow(yeniveri.Y - merkezler[i].Y, 2);
                gecici += Math.Pow(yeniveri.Z - merkezler[i].Z, 2);
                gecici += Math.Pow(yeniveri.S - merkezler[i].S, 2);
                gecici = Math.Sqrt(gecici);
                if (gecici < uzaklık)
                {
                    uzaklık = gecici;
                    kume = i + 1;
                }
            }
            Console.WriteLine("Girdiğiniz veriye ait bilgiler :");
            Console.WriteLine("Öznitelikler\tMin Uzaklık\tKüme No\tKüme Merkezi");
            
            Console.Write(yeniveri.ToString()+"\t");
            Console.Write(string.Format("{0:F6}",uzaklık)+"\t");
            Console.Write(kume + "\t");
            Console.Write(merkezler[kume - 1].ToString());
        }
    }
   

    
    class Veri
    {
        double x;
        double y;
        double z;
        double s;
        
        public Veri(double x, double y, double z, double s)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.s = s;
        }
        public Veri (double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
       


        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public double S { get => s; set => s = value; }

        public  bool Equals(Veri veri)
        {
            if((this.x == veri.x) && (this.y == veri.y) && (this.z == veri.z) && (this.s == veri.s))
            {
                return true;
            }
            return false;
        }
        override
        public string ToString()
        {
            if(this.S == 0)
            {
                return string.Format("{0,3} {1,3} {2,3}",Math.Round(this.X ,3), Math.Round(this.Y,3) , Math.Round(this.Z,3));
            }
            return Math.Round(this.X,3) + " " + Math.Round(this.Y,3) + " " + Math.Round(this.Z ,3)+ " " + Math.Round(this.S,3);
            
        }
    }
}