using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Effects;
namespace Balls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid[,] pole = new Grid[32, 32];//Vlastnost pole uchovaná ve dvojrozměrném poli pro identifikaci objektů typu Grid zastupující jedno pole hry. Díky tomu bude možné identifikovat pole, u kterého se budou chtít změnit jeho vlastnosti.
        private Game hra;// Instance jedné hry.
        private GameComposition sestavaHry = new GameComposition();//Instance aktuální hrané hry
        private CellCoordination sberacSouradnic;//Instance sběrače souřadnic, do kterého se uloží souřadnice pole, na které hráč kliknul, a to pro pozdější použití.
        private double SirkaIVyskaElipsy;// Vlastnost, ve které bude uchováný dynamicky vypočítaný průměr míče dle počtu řádků a sloupců desky.
        private bool RezimCeleObrazovky = false;
        // Začíná definice štetců příslušných barev.
        System.Windows.Media.SolidColorBrush SvetleZelenaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
        System.Windows.Media.SolidColorBrush CervenaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
        System.Windows.Media.SolidColorBrush TmaveModraStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
        System.Windows.Media.SolidColorBrush ZlutaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
        System.Windows.Media.SolidColorBrush SvetleModraStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Cyan);
        System.Windows.Media.SolidColorBrush FialovaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.BlueViolet);
        System.Windows.Media.SolidColorBrush HnedaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Maroon);
        System.Windows.Media.SolidColorBrush RuzovaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Pink);
        System.Windows.Media.SolidColorBrush ZelenaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.ForestGreen);
        System.Windows.Media.SolidColorBrush ZlataStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gold);
        System.Windows.Media.SolidColorBrush OranzovaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkOrange);
        System.Windows.Media.SolidColorBrush BilaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
        System.Windows.Media.SolidColorBrush SedivaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
        System.Windows.Media.SolidColorBrush CernaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
        System.Windows.Media.SolidColorBrush ModraStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DodgerBlue);
        System.Windows.Media.SolidColorBrush VojenskaZelenaStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Olive);
        
        LinearGradientBrush SvetleZelenaLinearniPrechodStetec= new LinearGradientBrush();
        LinearGradientBrush CervenaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush TmaveModraLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush ZlutaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush SvetleModraLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush FialovaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush HnedaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush RuzovaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush ZelenaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush ZlataLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush OranzovaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush BilaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush SedivaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush CernaLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush ModraLinearniPrechodStetec = new LinearGradientBrush();
        LinearGradientBrush VojenskaZelenaLinearniPrechodStetec = new LinearGradientBrush();

        LinearGradientBrush DuhoveLinearniPrechodStetec = new LinearGradientBrush();

        System.Windows.Media.SolidColorBrush pozadiPoleStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Silver);
        System.Windows.Media.SolidColorBrush pozadiZvyraznenePoleStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGray);
        System.Windows.Media.SolidColorBrush pozadiZvyrazneneCervenePoleStetec = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.IndianRed);

        private void SestavStetceSLinearnimPrechodem()
        {
            System.Windows.Media.Color[] svetlejsiBarvaPole = {
                Colors.LawnGreen,
                Colors.Red,
                Colors.Blue,
                Colors.Yellow,
                Colors.Cyan,
                Colors.BlueViolet,
                Colors.SaddleBrown,
                Colors.Pink,
                Colors.ForestGreen,
                Colors.Gold,
                Colors.DarkOrange,
                Colors.White,
                Colors.Gray,
                Color.FromRgb(95,95,95),
                Colors.DeepSkyBlue,
                Colors.Olive
                
            };
            System.Windows.Media.Color[] tmavsiBarvaPole = {
                Colors.Green,
                Colors.DarkRed,
                Colors.DarkBlue,
                Colors.Gold,
                Colors.DarkCyan,
                Colors.DarkViolet,
                Colors.Maroon,
                Colors.Plum,
                Colors.DarkGreen,
                Colors.Goldenrod,
                Colors.Chocolate,
                Colors.LightGray,
                Colors.DimGray,
                Color.FromRgb(32,32,32),
                Colors.DodgerBlue,
                Colors.DarkOliveGreen
            };
            LinearGradientBrush[] stetecSLinearnimPrechodemPole = {
            SvetleZelenaLinearniPrechodStetec,
            CervenaLinearniPrechodStetec,
            TmaveModraLinearniPrechodStetec,
            ZlutaLinearniPrechodStetec,
            SvetleModraLinearniPrechodStetec,
            FialovaLinearniPrechodStetec,
            HnedaLinearniPrechodStetec,
            RuzovaLinearniPrechodStetec,
            ZelenaLinearniPrechodStetec,
            ZlataLinearniPrechodStetec,
            OranzovaLinearniPrechodStetec,
            BilaLinearniPrechodStetec,
            SedivaLinearniPrechodStetec,
            CernaLinearniPrechodStetec,
            ModraLinearniPrechodStetec,
            VojenskaZelenaLinearniPrechodStetec
        };
            for (int i=0; i<=15;i++)
            { NastavStetecSLinearnimPrechodem(stetecSLinearnimPrechodemPole[i],svetlejsiBarvaPole[i],tmavsiBarvaPole[i]); }
            NastavStetecSDuhouSLinearnimPrechodem();        }
        private void NastavStetecSLinearnimPrechodem(LinearGradientBrush StetecSLinearnimPrechodem, System.Windows.Media.Color Color1, System.Windows.Media.Color Color2)
        {
            GradientStopCollection linearStops = new GradientStopCollection();

            GradientStop Stop1 = new GradientStop( Color1, 0.0);
            GradientStop Stop2 = new GradientStop(Color1, 0.1);
            GradientStop Stop3 = new GradientStop(Color2, 0.9);
            GradientStop Stop4 = new GradientStop(Color2, 1);

            linearStops.Add(Stop1);
            linearStops.Add(Stop2);
            linearStops.Add(Stop3);
            linearStops.Add(Stop4);

            StetecSLinearnimPrechodem.StartPoint = new Point(0.5, 0.1);
            StetecSLinearnimPrechodem.EndPoint = new Point(0.5, 0.9);

            StetecSLinearnimPrechodem.GradientStops = linearStops;
        }
        private void NastavStetecSDuhouSLinearnimPrechodem()
        {
            GradientStopCollection linearStops = new GradientStopCollection();

            GradientStop Stop1 = new GradientStop(Colors.Red, ((double)1 / 6) * 0);
            GradientStop Stop2 = new GradientStop(Colors.Orange, ((double)1 / 6) * 1);
            GradientStop Stop3 = new GradientStop(Colors.Yellow, ((double)1 / 6) * 2);
            GradientStop Stop4 = new GradientStop(Colors.Green, ((double)1 / 6) * 3);
            GradientStop Stop5 = new GradientStop(Colors.Blue, ((double)1 / 6) * 4);
            GradientStop Stop6 = new GradientStop(Colors.Purple, ((double)1 / 6) * 5);
            GradientStop Stop7 = new GradientStop(Colors.Magenta, ((double)1 / 6) * 6);
            
            linearStops.Add(Stop1);
            linearStops.Add(Stop2);
            linearStops.Add(Stop3);
            linearStops.Add(Stop4);
            linearStops.Add(Stop5);
            linearStops.Add(Stop6);
            linearStops.Add(Stop7);

            DuhoveLinearniPrechodStetec.StartPoint = new Point(0.5, 0.0);
            DuhoveLinearniPrechodStetec.EndPoint = new Point(0.5, 1);

            DuhoveLinearniPrechodStetec.GradientStops = linearStops;
        }
        public MainWindow()
        {
            SestavStetceSLinearnimPrechodem();
            InitializeComponent();
            NovaHra();// Při inicializaci okna se spustí nová hra.
        }

        private void NovaHra()// Tato metoda se provede vždy, když začne nová hra.
        {
            ResetujProstredi();//Musí se vyčistit okno od všeho, co v okně zůstalo po nové hře
            hra = new Game(
        this.sestavaHry,
        sestavaHry.VratVyska(),
        sestavaHry.VratSirka(),
        sestavaHry.VratSvetleZelena(),
        sestavaHry.VratCervena(),
        sestavaHry.VratTmaveModra(),
        sestavaHry.VratZluta(),
        sestavaHry.VratSvetleModra(),
        sestavaHry.VratFialova(),
        sestavaHry.VratHneda(),
        sestavaHry.VratRuzova(),
        sestavaHry.VratZelena(),
        sestavaHry.VratZlata(),
        sestavaHry.VratOranzova(),
        sestavaHry.VratBila(),
        sestavaHry.VratSediva(),
        sestavaHry.VratCerna(),
        sestavaHry.VratModra(),
        sestavaHry.VratVojenskaZelena(),
        sestavaHry.VratPocetHazenychMicuNaZacatkuHry(),
        sestavaHry.VratPocetHazenychMicuBehemHry(),
        sestavaHry.VratDuhoveBalls(),
        sestavaHry.VratZdvojnasobujiciBalls(),
        sestavaHry.VratTvarSkupinyMicuKteraExploduje(),
        sestavaHry.VratMinimalniDelkaLinky()

                );//Vytvoří hru a to podle toho, jaké vlastnosti má vlastnost okna sestavyHry.
            int sirkaDesky = 520;
            double jakyBudePodilPrumeruBallsOprotiDelcePole = 0.75;
            this.SirkaIVyskaElipsy = jakyBudePodilPrumeruBallsOprotiDelcePole * sirkaDesky / Math.Max(hra.VratVysku(), hra.VratSirku());// Zde se vypočte a uloží hodnota, která bude znamenat šířku a výšku míče, která se vypočte dynamicky podle výšky a šířky desky

            ProvestPrikazy();//To, co vypočítala logická vrstva, se nyní projeví ve vrstvě aplikační
        }
        public String ProvestPrikaz()// Provede jeden příkaz ve frontě příkazů z logické vrstvy
        {
            String prikaz = hra.VratPrikaz();// Vezme si jeden příkaz z fronty příkazů v hře.
            //MessageBox.Show(prikaz);
            string[] parametrPrikazu = prikaz.Split(' ');//Zde je příkaz rozdělen po mezerách na jednotlivé parametry
            switch (parametrPrikazu[0])//Zde se zkoumá 0. parametr
            {
                case "HRA":
                    switch (parametrPrikazu[1])//Zde se zkoumá 1. parametr
                    {
                        case "NOVA":
                            { ResetujProstredi(); }
                            break;

                        case "KONEC":
                            ZadejteVaseJmenoWindow zadejteVaseJmenoWindow = new ZadejteVaseJmenoWindow(this.hra);
                            zadejteVaseJmenoWindow.ShowDialog();// Zobrazí dialog pro zadání jména hráče.

                            NovaHra();//Hra skončila, výsledky se uložily, nyní začíná nová hra
                            break;
                        default:
                            ;
                            break;
                    }
                    break;

                case "DESKA":
                    {
                        VykresliDesku(Int32.Parse(parametrPrikazu[1]), Int32.Parse(parametrPrikazu[2]));

                    };
                    break;
                case "DNO":
                    {

                    };
                    break;
                case "MIC":
                    {
                        switch (parametrPrikazu[3])//Zde se zkoumá 3. parametr
                        {
                            case "NOVY":
                                {
                                    VytvorNovyMic(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2]), parametrPrikazu[4], parametrPrikazu[5]);
                                }
                                break;
                            case "SKAKEJ":
                                MiciSkakej(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2]));
                                break;
                            case "NESKAKEJ":
                                MiciNeskakej(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2])); break;
                            case "ODSTRANIT":
                                VycistitPole(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2])); break;
                            case "DALSI1":
                                {
                                    switch(Convert.ToInt32(parametrPrikazu[2]))
                                    {
                                        case 1: dalsiMic1.Fill = SvetleZelenaLinearniPrechodStetec; break;
                                        case 2: dalsiMic1.Fill = CervenaLinearniPrechodStetec; break;
                                        case 3: dalsiMic1.Fill = TmaveModraLinearniPrechodStetec; break;
                                        case 4: dalsiMic1.Fill = ZlutaLinearniPrechodStetec; break;
                                        case 5: dalsiMic1.Fill = SvetleModraLinearniPrechodStetec; break;
                                        case 6: dalsiMic1.Fill = FialovaLinearniPrechodStetec; break;
                                        case 7: dalsiMic1.Fill = HnedaLinearniPrechodStetec; break;
                                    }
                                }
                                 break;
                            case "DALSI2":
                                switch (Convert.ToInt32(parametrPrikazu[2]))
                                {
                                    case 1: dalsiMic2.Fill = SvetleZelenaLinearniPrechodStetec; break;
                                    case 2: dalsiMic2.Fill = CervenaLinearniPrechodStetec; break;
                                    case 3: dalsiMic2.Fill = TmaveModraLinearniPrechodStetec; break;
                                    case 4: dalsiMic2.Fill = ZlutaLinearniPrechodStetec; break;
                                    case 5: dalsiMic2.Fill = SvetleModraLinearniPrechodStetec; break;
                                    case 6: dalsiMic2.Fill = FialovaLinearniPrechodStetec; break;
                                    case 7: dalsiMic2.Fill = HnedaLinearniPrechodStetec; break;
                                }
                                break;
                            case "DALSI3":
                                switch (Convert.ToInt32(parametrPrikazu[2]))
                                {
                                    case 1: dalsiMic3.Fill = SvetleZelenaLinearniPrechodStetec; break;
                                    case 2: dalsiMic3.Fill = CervenaLinearniPrechodStetec; break;
                                    case 3: dalsiMic3.Fill = TmaveModraLinearniPrechodStetec; break;
                                    case 4: dalsiMic3.Fill = ZlutaLinearniPrechodStetec; break;
                                    case 5: dalsiMic3.Fill = SvetleModraLinearniPrechodStetec; break;
                                    case 6: dalsiMic3.Fill = FialovaLinearniPrechodStetec; break;
                                    case 7: dalsiMic3.Fill = HnedaLinearniPrechodStetec; break;
                                }
                                break;
                            default: ;break;
                        }
                    };
                    break;
                case "POLE":
                    {
                        switch (parametrPrikazu[3])//Zde se zkoumá 3. parametr
                        {
                            case "POZADI":
                                {
                                    switch (parametrPrikazu[4])
                                    {
                                        case "ZVYRAZNENE": { ZvyrazniPole(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2])); }; break;
                                        case "NEZVYRAZNENE": { ZnevyrazniPole(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2])); }; break;
                                        case "CERVENE": { ZvyrazniCervenePole(Convert.ToInt32(parametrPrikazu[1]), Convert.ToInt32(parametrPrikazu[2])); }; break;
                                        default: { }; break;
                                    }
                                }; break;
                        }
                    }; break;
                case "VYSLEDEK":
                    { NastavVysledek(Convert.ToInt32(parametrPrikazu[1])); }
                    break;
                default:
                    MessageBox.Show("Chyba: Logická vrstva vygenerovala změnu, které grafická vrstva nerozumí"); ;
                    break;

            }
            return prikaz;
        }
        /*
        -------------------------------------------------------------------------------
        Začíná popis syntaxe příkazů speciálního jazyka, který generuje logická vrstva.
        -------------------------------------------------------------------------------
        Tím, co generuje logická vrstva, informuje ostatní o tom, jaké prvky se změnily a jak.
        Velkými písmeny jsou slova příkazu. 
        To, co je ve spičatých závorkách, je nutné nahradit konkrétním číslem nebo textovým řetězcem. 
        Řetězce se zapisují bez dvojitých nebo jednoduchých uvozovek.
        Příklad: Následující příkaz zobrazí nový černý míč v poli 4. řádku a 6. sloupce a to tak, že použije efekt nafouknutí.
        MIC 4 6 NOVY CERNY NAFOUKNOUT
        ------------------------------------------------------------------------------

        Příkaz:   HRA NOVA
        Popis:    Prezentační vrstva musí na rozkaz tohoto příkazu připravit prezentační vrstvu pro novou hru a smazat vše, co zbylo po hře minulé.

        Příkaz:   HRA KONEC
        Popis:    Prezentační vrstva musí na rozkaz tohoto příkazu připravit prezentační vrstvu pro operace prováděné po dohrání hry.

        Příkaz:   DESKA <výška> <šířka> 
        Popis:    Tento příkaz musí následovat ihned za příkazem HRA NOVA. Podle tohoto příkazu prezentační vrstva sestaví desku o daných rozměrech.

        Příkaz:   DNO
        Popis:    Tento příkaz negeneruje logická vrstva, tento příkaz si generuje prezentační vrstva automaticky, pokud již byly všechny příkazy ve frontě příkazů vykonány a tato fronta je nyní prázdná. Aplikační vrstva je tímto příkazem informována, že je aktuální podoba prezentační vrstvy shodná s tím, jak jsou prvky hry reprezentovány v logické vrstvě.

        Příkaz:   MIC <řádek> <sloupec> NOVY <typ> NAFOUKNOUT
        Popis:    V desce se na dané pozici objeví nový míč daného typu a bude nafouknut, což znamená, že bude nejdříve malý a postupně se bude zvětšovat do konečné podoby.

        Příkaz:   MIC <řádek> <sloupec> NOVY <typ> 
        Popis:    V desce se na dané pozici objeví nový míč daného typu a nebude nafouknut, což znamená, že se zobrazí do konečné podoby již nafouknutý.

        Příkaz:   MIC <řádek> <sloupec> SKAKEJ
        Popis:    Daný míč začne skákat.

        Příkaz:   MIC <řádek> <sloupec> NESKAKEJ
        Popis:    Daný míč přestane skákat.

        Příkaz:   MIC <řádek> <sloupec> ODSTRANIT
        Popis:    Daný míč bude odstraněn.
            
        Příkaz:   POLE <řádek> <sloupec> POZADI ZVYRAZNENE
        Popis:    Dané pole bude zvýrazněno.

        Příkaz:   POLE <řádek> <sloupec> POZADI NEZVYRAZNENE
        Popis:    Dané pole bude nezvýrazněno.

        Příkaz:   POLE <řádek> <sloupec> POZADI CERVENE
        Popis:    Dané pole bude zvýrazněno červeně po výbuchu.

        Příkaz:   VYSLEDEK <počet bodů>
        Popis:    Informuje, že se počet bodů aktuálního výsledku změnil.

        ------------------------------------------------------------------------------
        Končí popis syntaxe příkazů speciálního jazyka, který generuje logická vrstva.
        ------------------------------------------------------------------------------
        */


        public void ProvestPrikazy() // Provede všechny příkazy z fronty příkazů.
        {
            bool pokracovat = true;
            do
            {// Dělej.
                if (ProvestPrikaz() == "DNO") { pokracovat = false; };
            }//Pokud příkaz ve frontě příkazů není DNO, neměň hodnotu proměnné pokracovat, jinak ji nastav na true.
            while (pokracovat);// Dokud je hodnota proměnné pokracovat pravdivá.

        }
        private void ZvyrazniPole(int Radek, int Sloupec)//Nastaví v prezentační vrstvě dané pozadí daného pole.
        {
            pole[Radek - 1, Sloupec - 1].Background = pozadiZvyraznenePoleStetec;
        }
        private void ZnevyrazniPole(int Radek, int Sloupec)//Nastaví v prezentační vrstvě dané pozadí daného pole.
        {
            pole[Radek - 1, Sloupec - 1].Background = pozadiPoleStetec;
        }
        private void ZvyrazniCervenePole(int Radek, int Sloupec)//Nastaví v prezentační vrstvě dané pozadí daného pole.
        {
            pole[Radek - 1, Sloupec - 1].Background = pozadiZvyrazneneCervenePoleStetec;
        }
        public void VytvorNovyMic(int Radek, int Sloupec, String Typ, String Efekt)// Vytvoří v prezentační vrstvě nový míč v daném poli daného typu a s daným efektem.
                                                                                   //Vždy před zahájením nové hry tato metoda nastaví grafické prostředí do výchozí podoby. Jinými slovy vyčistí to, co zůstalo po předchozí hře.
        {
            Ellipse NovyMic = new Ellipse();
            NovyMic.Height = SirkaIVyskaElipsy;
            NovyMic.Width = SirkaIVyskaElipsy;
            TextBlock popisekTextBlock = new TextBlock { Text = "2×", FontSize = 24, VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center };//Pokud je míč zdvojnásobující, bude mít před sebou tento popisek.
            switch (Typ)// Podle typu míče se nastaví příslušný míč.
            {
                case "SVETLEZELENA":
                    {
                        NovyMic.Fill = SvetleZelenaLinearniPrechodStetec;
                    }; break;
                case "CERVENA":
                    {
                        NovyMic.Fill = CervenaLinearniPrechodStetec;
                    }; break;
                case "TMAVEMODRA":
                    {
                        NovyMic.Fill = TmaveModraLinearniPrechodStetec;
                    }; break;
                case "ZLUTA":
                    {
                        NovyMic.Fill = ZlutaLinearniPrechodStetec;
                    }; break;
                case "SVETLEMODRA":
                    {
                        NovyMic.Fill = SvetleModraLinearniPrechodStetec;
                    }; break;
                case "FIALOVA":
                    {
                        NovyMic.Fill = FialovaLinearniPrechodStetec;
                    }; break;
                case "HNEDA":
                    {
                        NovyMic.Fill = HnedaLinearniPrechodStetec;
                    }; break;
                case "RUZOVA":
                    {
                        NovyMic.Fill = RuzovaLinearniPrechodStetec;
                    }; break;
                case "ZELENA":
                    {
                        NovyMic.Fill = ZelenaLinearniPrechodStetec;
                    }; break;
                case "ZLATA":
                    {
                        NovyMic.Fill = ZlataLinearniPrechodStetec;
                    }; break;
                case "ORANZOVA":
                    {
                        NovyMic.Fill = OranzovaLinearniPrechodStetec;
                    }; break;
                case "BILA":
                    {
                        NovyMic.Fill = BilaLinearniPrechodStetec;
                    }; break;
                case "SEDIVA":
                    {
                        NovyMic.Fill = SedivaLinearniPrechodStetec;
                    }; break;
                case "CERNA":
                    {
                        NovyMic.Fill = CernaLinearniPrechodStetec;
                    }; break;
                case "MODRA":
                    {
                        NovyMic.Fill = ModraLinearniPrechodStetec;
                    }; break;
                case "VOJENSKAZELENA":
                    {
                        NovyMic.Fill = VojenskaZelenaLinearniPrechodStetec;
                    }; break;
                case "DUHOVE":
                    {
                        NovyMic.Fill = DuhoveLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICISVETLEZELENA":
                    {
                        NovyMic.Fill = SvetleZelenaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICICERVENA":
                    {
                        NovyMic.Fill = CervenaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICITMAVEMODRA":
                    {
                        NovyMic.Fill = TmaveModraLinearniPrechodStetec;
                        popisekTextBlock.Foreground = BilaStetec;// Zde se musí nastavit barva textu na jinou než černou, aby bylo vidět, že je míč zdvojnásobující.
                    }; break;
                case "ZDVOJNASOBUJICIZLUTA":
                    {
                        NovyMic.Fill = ZlutaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICISVETLEMODRA":
                    {
                        NovyMic.Fill = SvetleModraLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIFIALOVA":
                    {
                        NovyMic.Fill = FialovaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIHNEDA":
                    {
                        NovyMic.Fill = HnedaLinearniPrechodStetec;
                        popisekTextBlock.Foreground = BilaStetec;// Zde se musí nastavit barva textu na jinou než černou, aby bylo vidět, že je míč zdvojnásobující.
                    }; break;
                case "ZDVOJNASOBUJICIRUZOVA":
                    {
                        NovyMic.Fill = RuzovaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIZELENA":
                    {
                        NovyMic.Fill = ZelenaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIZLATA":
                    {
                        NovyMic.Fill = ZlataLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIORANZOVA":
                    {
                        NovyMic.Fill = OranzovaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIBILA":
                    {
                        NovyMic.Fill = BilaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICISEDIVA":
                    {
                        NovyMic.Fill = SedivaLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICICERNA":
                    {
                        NovyMic.Fill = CernaLinearniPrechodStetec;
                        popisekTextBlock.Foreground = BilaStetec;// Zde se musí nastavit barva textu na jinou než černou, aby bylo vidět, že je míč zdvojnásobující.
                    }; break;
                case "ZDVOJNASOBUJICIMODRA":
                    {
                        NovyMic.Fill = ModraLinearniPrechodStetec;
                    }; break;
                case "ZDVOJNASOBUJICIVOJENSKAZELENA":
                    {
                        NovyMic.Fill = VojenskaZelenaLinearniPrechodStetec;
                    }; break;
                default:
                    {
                        NovyMic.Fill = CervenaStetec;
                    }; break;
            }
            if (Efekt == "NAFOUKNOUT")//Pokud se míč objevil, nepřesouvá se, je potřeba jej nafouknout
            {
                NovyMic.Height = (int)(SirkaIVyskaElipsy / 2.5);// Nejprve se míč objeví nenafouknutý.
                NovyMic.Width = (int)(SirkaIVyskaElipsy / 2.5);// Šířka je stejná jako výška. Šířka a výška se mění pouze, pokud míč skáče, zde míč neskáče.

            }
            pole[Radek - 1, Sloupec - 1].Children.Add(NovyMic);// Daný míč se přidá do příslušného pole.
            if (Typ.Contains("ZDVOJNASOBUJICI")) { pole[Radek - 1, Sloupec - 1].Children.Add(popisekTextBlock); };//Pokud je míč zdvojnásobující, bude mít před sebou tento popisek.

            if (Efekt == "NAFOUKNOUT")//Pokud se míč objevil, nepřesouvá se, je potřeba jej nafouknout
            {
                double podilPrumeruBallsPredNafouknutimOprotiPrumeruPoNafouknuti = 2.5;
                double trvani = 0.25;
                DoubleAnimation nafouknutiAnimace = new DoubleAnimation();
                nafouknutiAnimace.From = (int)(SirkaIVyskaElipsy / podilPrumeruBallsPredNafouknutimOprotiPrumeruPoNafouknuti);
                nafouknutiAnimace.To = SirkaIVyskaElipsy;
                nafouknutiAnimace.Duration = new Duration(TimeSpan.FromSeconds(trvani));
                nafouknutiAnimace.AutoReverse = false;// Animace se nebude opakovat
                NovyMic.BeginAnimation(Ellipse.HeightProperty, nafouknutiAnimace);
                NovyMic.BeginAnimation(Ellipse.WidthProperty, nafouknutiAnimace);
            }

        }
        private void MiciSkakej(int Radek, int Sloupec)// Tato metoda má za účel zapnout animaci elipsy vytvářející efekt skákání.
        {
            Ellipse SkakajiciMic = pole[Radek - 1, Sloupec - 1].Children.OfType<Ellipse>().FirstOrDefault();
            double trvaniAnimace = 300;
            double minimalniPrumerBalls = 0.8;
            DoubleAnimation animaceVysky = new DoubleAnimation();
            animaceVysky.From = SirkaIVyskaElipsy * minimalniPrumerBalls;
            animaceVysky.To = SirkaIVyskaElipsy;
            animaceVysky.RepeatBehavior = RepeatBehavior.Forever;
            animaceVysky.Duration = new Duration(TimeSpan.FromMilliseconds(trvaniAnimace));
            animaceVysky.AutoReverse = true;

            DoubleAnimation animaceSirky = new DoubleAnimation();
            animaceSirky.From = SirkaIVyskaElipsy;
            animaceSirky.To = SirkaIVyskaElipsy * minimalniPrumerBalls;
            animaceSirky.RepeatBehavior = RepeatBehavior.Forever;
            animaceSirky.Duration = new Duration(TimeSpan.FromMilliseconds(trvaniAnimace));
            animaceSirky.AutoReverse = true;

            double hodnotaOd = SirkaIVyskaElipsy * 0.2;
            double hodnotaDo = -2;
            TranslateTransform skokTransform = new TranslateTransform();
            SkakajiciMic.RenderTransform = skokTransform;
            DoubleAnimation skokAnimation = new DoubleAnimation(hodnotaOd, hodnotaDo, TimeSpan.FromMilliseconds(trvaniAnimace));
            skokAnimation.RepeatBehavior = RepeatBehavior.Forever;
            skokAnimation.AutoReverse = true;

            skokTransform.BeginAnimation(TranslateTransform.YProperty, skokAnimation);
            SkakajiciMic.BeginAnimation(Ellipse.HeightProperty, animaceVysky);
            SkakajiciMic.BeginAnimation(Ellipse.WidthProperty, animaceSirky);
        }
        private void MiciNeskakej(int Radek, int Sloupec)
        {
            Ellipse SkakajiciMic = pole[Radek - 1, Sloupec - 1].Children.OfType<Ellipse>().FirstOrDefault();
            SkakajiciMic.RenderTransform = null;
            SkakajiciMic.BeginAnimation(Ellipse.HeightProperty, null);
            SkakajiciMic.BeginAnimation(Ellipse.WidthProperty, null);
            SkakajiciMic.Height = SirkaIVyskaElipsy;
            SkakajiciMic.Width = SirkaIVyskaElipsy;
        }
        private void VycistitPole(int Radek, int Sloupec)// Odstraní všechny děti tohoto pole.
        {
            pole[Radek - 1, Sloupec - 1].Children.Clear();
        }

        public void ResetujProstredi()//Vždy před zahájením nové hry tato metoda nastaví grafické prostředí do výchozí podoby. Jinými slovy vyčistí to, co zůstalo po předchozí hře.
        {
            deskaGrid.Children.Clear();
            deskaGrid.ColumnDefinitions.Clear();
            deskaGrid.RowDefinitions.Clear();
            vysledekLabel.Content = 0;// Výsledek se nastaví na nulu.
        }
        public void NastavVysledek(int AktualniVysledek) //Nastaví výsledek do aktuální podoby.
        { vysledekLabel.Content = AktualniVysledek; }

        public void VykresliDesku(int vyska, int sirka)//Sestaví pole desky v aplikační vrstvě do kontrolky typu Grid s názvem Deska
        {
            int maximumRozmeru = Math.Max(vyska, sirka);
            //začátek: sestavení desky
            for (int i = 0; i <= maximumRozmeru - 1; i++) //Začíná přidávání sloupců a řádků.
            {
                ColumnDefinition columnDefinition1 = new ColumnDefinition();
                deskaGrid.ColumnDefinitions.Add(columnDefinition1);

                RowDefinition rowDefinition1 = new RowDefinition();
                deskaGrid.RowDefinitions.Add(rowDefinition1);
            };
            //konec: sestavení desky
            //Začíná přidávání řádků a sloupců identifikovanými řádkem a sloupcem
            for (int r = 0; r <= maximumRozmeru - 1; r++)
            {

                for (int s = 0; s <= maximumRozmeru - 1; s++)
                {

                    pole[r, s] = new Grid();
                    deskaGrid.Children.Add(pole[r, s]);
                    Grid.SetRow(pole[r, s], r);
                    Grid.SetColumn(pole[r, s], s);
                    //Začíná přidávání manipulátorů k polím v aplikační vrstě, když hráč klikne na nějaké pole, toto pole informuje logickou vrstvu o tom, že bylo aktivováno a používá ke své identifikaci souřadnice řádku a sloupce daného pole.
                    switch (s + 1)
                    {
                        case 1: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec1)); } break;
                        case 2: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec2)); } break;
                        case 3: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec3)); } break;
                        case 4: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec4)); } break;
                        case 5: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec5)); } break;
                        case 6: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec6)); } break;
                        case 7: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec7)); } break;
                        case 8: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec8)); } break;
                        case 9: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec9)); } break;
                        case 10: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec10)); } break;
                        case 11: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec11)); } break;
                        case 12: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec12)); } break;
                        case 13: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec13)); } break;
                        case 14: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec14)); } break;
                        case 15: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec15)); } break;
                        case 16: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec16)); } break;
                        case 17: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec17)); } break;
                        case 18: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec18)); } break;
                        case 19: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec19)); } break;
                        case 20: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec20)); } break;
                        case 21: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec21)); } break;
                        case 22: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec22)); } break;
                        case 23: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec23)); } break;
                        case 24: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec24)); } break;
                        case 25: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec25)); } break;
                        case 26: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec26)); } break;
                        case 27: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec27)); } break;
                        case 28: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec28)); } break;
                        case 29: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec29)); } break;
                        case 30: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec30)); } break;
                        case 31: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec31)); } break;
                        case 32: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaSloupec32)); } break;
                        default: { } break;
                    };
                    switch (r + 1)
                    {
                        case 1: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek1)); } break;
                        case 2: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek2)); } break;
                        case 3: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek3)); } break;
                        case 4: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek4)); } break;
                        case 5: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek5)); } break;
                        case 6: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek6)); } break;
                        case 7: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek7)); } break;
                        case 8: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek8)); } break;
                        case 9: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek9)); } break;
                        case 10: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek10)); } break;
                        case 11: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek11)); } break;
                        case 12: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek12)); } break;
                        case 13: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek13)); } break;
                        case 14: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek14)); } break;
                        case 15: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek15)); } break;
                        case 16: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek16)); } break;
                        case 17: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek17)); } break;
                        case 18: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek18)); } break;
                        case 19: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek19)); } break;
                        case 20: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek20)); } break;
                        case 21: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek21)); } break;
                        case 22: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek22)); } break;
                        case 23: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek23)); } break;
                        case 24: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek24)); } break;
                        case 25: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek25)); } break;
                        case 26: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek26)); } break;
                        case 27: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek27)); } break;
                        case 28: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek28)); } break;
                        case 29: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek29)); } break;
                        case 30: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek30)); } break;
                        case 31: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek31)); } break;
                        case 32: { pole[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(KliknutoNaRadek32)); } break;
                        default: { } break;
                    };

                    pole[r, s].Margin = new Thickness(1);// Nastaví se délka vnější vzdálenosti prostoru pole.
                    if ((r <= vyska - 1) && (s <= sirka - 1))//Pokud je pole mimo pole, s kterými se bude hrát,
                    { pole[r, s].Background = pozadiPoleStetec; }
                    else
                    { pole[r, s].Visibility = Visibility.Collapsed; }// tak se viditelnost pole nastaví na zbořené.

                };
            };
            //konec: naplnění desky poli

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void PrepnoutDoRezimuCeleObrazovky()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            rezimObrazovkyMenuItem.Header = "Režim okna";
        }
        private void PrepnoutDoRezimuOkna()
        {
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.CanResizeWithGrip;
            rezimObrazovkyMenuItem.Header = "Režim celé obrazovky";
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (RezimCeleObrazovky)
            {
                PrepnoutDoRezimuOkna();
            }
            else

            {
                PrepnoutDoRezimuCeleObrazovky();
            }
            RezimCeleObrazovky = !RezimCeleObrazovky;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)// Metoda, která zajišťuje, že po stisknutí určitých kláves se něco provede.
        {
            switch (e.Key)
            {
                case Key.F1:
                    {
                        NapovedaWindow napovedaWindow = new NapovedaWindow();
                        napovedaWindow.Show();
                    }; break;
                case Key.F4: { novaHraSVychozimNastavenimNastavenimMenuItem_Click(sender, e); }; break;
                case Key.F5: { MenuItem_Click_5(sender, e); }; break;
                case Key.F6: { novaHraMenuItem_Click(sender, e); }; break;
                case Key.F8: { MenuItem_Click_4(sender, e); }; break;
                case Key.F11: { MenuItem_Click(sender, e); }; break;
            }
        }

        private void novaHraMenuItem_Click(object sender, RoutedEventArgs e)// Otevře se okno, kde si hráč nastaví hru, kterou bude hrát.
        {
            NovaHraSVlastnimNastavenimWindow novaHraSVlastnimNastavenimWindow = new NovaHraSVlastnimNastavenimWindow(this.sestavaHry);
            novaHraSVlastnimNastavenimWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();// Ukončí úplně aplikaci Míče.
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        //Začínají události kliknutí na určitý sloupec nebo řádek, tyto události posílají souřadnice sběrači souřadnic. Když jsou sběrači souřadnic poslány všechny souřadnice, tak se následovně logické vrstvě pošle informace, že bylo aktivované určité pole
        private void SouradniceSloupceDoSberaceSouradnic(int CisloSloupce)
        {
            sberacSouradnic = new CellCoordination();
            sberacSouradnic.VlozSouradniciSloupce(CisloSloupce);
        }
        private void KliknutoNaSloupec1(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(1); }
        private void KliknutoNaSloupec2(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(2); }
        private void KliknutoNaSloupec3(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(3); }
        private void KliknutoNaSloupec4(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(4); }
        private void KliknutoNaSloupec5(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(5); }
        private void KliknutoNaSloupec6(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(6); }
        private void KliknutoNaSloupec7(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(7); }
        private void KliknutoNaSloupec8(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(8); }
        private void KliknutoNaSloupec9(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(9); }
        private void KliknutoNaSloupec10(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(10); }
        private void KliknutoNaSloupec11(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(11); }
        private void KliknutoNaSloupec12(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(12); }
        private void KliknutoNaSloupec13(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(13); }
        private void KliknutoNaSloupec14(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(14); }
        private void KliknutoNaSloupec15(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(15); }
        private void KliknutoNaSloupec16(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(16); }
        private void KliknutoNaSloupec17(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(17); }
        private void KliknutoNaSloupec18(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(18); }
        private void KliknutoNaSloupec19(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(19); }
        private void KliknutoNaSloupec20(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(20); }
        private void KliknutoNaSloupec21(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(21); }
        private void KliknutoNaSloupec22(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(22); }
        private void KliknutoNaSloupec23(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(23); }
        private void KliknutoNaSloupec24(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(24); }
        private void KliknutoNaSloupec25(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(25); }
        private void KliknutoNaSloupec26(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(26); }
        private void KliknutoNaSloupec27(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(27); }
        private void KliknutoNaSloupec28(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(28); }
        private void KliknutoNaSloupec29(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(29); }
        private void KliknutoNaSloupec30(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(30); }
        private void KliknutoNaSloupec31(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(31); }
        private void KliknutoNaSloupec32(object sender, RoutedEventArgs e) { SouradniceSloupceDoSberaceSouradnic(32); }


        private void SouradniceRadkuDoSberaceSouradnic(int CisloRadku)
        {
            sberacSouradnic.VlozSouradniciRadku(CisloRadku);
            hra.AktivujPole(sberacSouradnic.VratSouradniciRadku(), sberacSouradnic.VratSouradniciSloupce());
            ProvestPrikazy();
        }

        private void KliknutoNaRadek1(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(1); }
        private void KliknutoNaRadek2(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(2); }
        private void KliknutoNaRadek3(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(3); }
        private void KliknutoNaRadek4(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(4); }
        private void KliknutoNaRadek5(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(5); }
        private void KliknutoNaRadek6(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(6); }
        private void KliknutoNaRadek7(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(7); }
        private void KliknutoNaRadek8(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(8); }
        private void KliknutoNaRadek9(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(9); }
        private void KliknutoNaRadek10(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(10); }
        private void KliknutoNaRadek11(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(11); }
        private void KliknutoNaRadek12(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(12); }
        private void KliknutoNaRadek13(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(13); }
        private void KliknutoNaRadek14(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(14); }
        private void KliknutoNaRadek15(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(15); }
        private void KliknutoNaRadek16(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(16); }
        private void KliknutoNaRadek17(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(17); }
        private void KliknutoNaRadek18(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(18); }
        private void KliknutoNaRadek19(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(19); }
        private void KliknutoNaRadek20(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(20); }
        private void KliknutoNaRadek21(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(21); }
        private void KliknutoNaRadek22(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(22); }
        private void KliknutoNaRadek23(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(23); }
        private void KliknutoNaRadek24(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(24); }
        private void KliknutoNaRadek25(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(25); }
        private void KliknutoNaRadek26(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(26); }
        private void KliknutoNaRadek27(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(27); }
        private void KliknutoNaRadek28(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(28); }
        private void KliknutoNaRadek29(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(29); }
        private void KliknutoNaRadek30(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(30); }
        private void KliknutoNaRadek31(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(31); }
        private void KliknutoNaRadek32(object sender, RoutedEventArgs e) { SouradniceRadkuDoSberaceSouradnic(32); }
        
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)//Otevře se okno O ...
        {
            AboutWindow oWindow = new AboutWindow();
            oWindow.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)//Otevře se okno Nápovědy.
        {
            NapovedaWindow napovedaWindow = new NapovedaWindow();
            napovedaWindow.ShowDialog();
        }

        private void novaHraSVychozimNastavenimNastavenimMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NovaHra();
        }

        private void HlavniOknoWindow_Activated(object sender, EventArgs e)// Pokud byla po aktivaci okna sestava hry změněna, spustí se nová hra s touto sestavou.
        {
            if (sestavaHry.VratZmeneno())
            {
                sestavaHry.NastavZmeneno(false);
                NovaHra();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            InformaceOAktualniHreWindow informaceOAktualniHreWindow = new InformaceOAktualniHreWindow(this.sestavaHry);
            informaceOAktualniHreWindow.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            PrehledVysledkuWindow prehledVysledkuWindow = new PrehledVysledkuWindow(this.hra, this.sestavaHry);
            prehledVysledkuWindow.ShowDialog();
        }
        
    }
}