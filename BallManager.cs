using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Balls
{
    class BallManager
    {
        List<String> PouziteTypyMicu = new List<String>();// Zde jsou registrovány všechny použité typy míčů, budou se generovat pouze míče, jejichž typy jsou v tomto registru.
        private String[] PolePouzitychBarev = new String [16];
        private int IndexPolePouzitychBarev = 0;
        public Queue<Ball> DalsiBalls = new Queue<Ball>();
        public Ball DalsiMic1 = null;
        public Ball DalsiMic2 = null;
        public Ball DalsiMic3 = null;
        public BallManager(
       bool SvetleZelena,
       bool Cervena,
       bool TmaveModra,
       bool Zluta,
       bool SvetleModra,
       bool Fialova,
       bool Hneda,
       bool Ruzova,
       bool Zelena,
       bool Zlata,
       bool Oranzova,
       bool Bila,
       bool Sediva,
       bool Cerna,
       bool Modra,
       bool VojenskaZelena,
       bool DuhoveBalls,
       bool ZdvojnasobujiciBalls,
       Game hra)
        {
            NaplnPolePouzitychBarev(
       SvetleZelena,
       Cervena,
       TmaveModra,
       Zluta,
       SvetleModra,
       Fialova,
       Hneda,
       Ruzova,
       Zelena,
       Zlata,
       Oranzova,
       Bila,
       Sediva,
       Cerna,
       Modra,
       VojenskaZelena); 
            NaplnSeznamPouzitychTypuMicuDanymBallsmPokudJsouSplnenyPodminky(DuhoveBalls,ZdvojnasobujiciBalls);
            DalsiMic1 = VygenerujNovyMic();
            DalsiMic2 = VygenerujNovyMic();
            DalsiMic3 = VygenerujNovyMic();
            DalsiBalls.Enqueue(DalsiMic1);
            DalsiBalls.Enqueue(DalsiMic2);
            DalsiBalls.Enqueue(DalsiMic3);

            
            String I = "";

            switch (DalsiMic1.VratBarvu())
            {
                case "SvetleZelena": { I = "1"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); } break;
                case "Cervena": { I = "2"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "TmaveModra": { I = "3"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Zluta": { I = "4"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "SvetleModra": { I = "5"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Fialova": { I = "6"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Hneda": { I = "7"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI1")); }; break;

            }

            switch (DalsiMic2.VratBarvu())
            {
                case "SvetleZelena": { I = "1"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); } break;
                case "Cervena": { I = "2"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "TmaveModra": { I = "3"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Zluta": { I = "4"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "SvetleModra": { I = "5"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Fialova": { I = "6"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Hneda": { I = "7"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI2")); }; break;

            }

            switch (DalsiMic3.VratBarvu())
            {
                case "SvetleZelena": { I = "1"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); } break;
                case "Cervena": { I = "2"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "TmaveModra": { I = "3"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Zluta": { I = "4"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "SvetleModra": { I = "5"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Fialova": { I = "6"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Hneda": { I = "7"; hra.VlozPrikaz(String.Concat("MIC X ", I, " DALSI3")); }; break;

            }
        }
        private void NaplnPolePouzitychBarev(
       bool SvetleZelena,
       bool Cervena,
       bool TmaveModra,
       bool Zluta,
       bool SvetleModra,
       bool Fialova,
       bool Hneda,
       bool Ruzova,
       bool Zelena,
       bool Zlata,
       bool Oranzova,
       bool Bila,
       bool Sediva,
       bool Cerna,
       bool Modra,
       bool VojenskaZelena)
        {
            Boolean[] polePouzitychBarevVLogickychHodnotach = {
       SvetleZelena,
       Cervena,
       TmaveModra,
       Zluta,
       SvetleModra,
       Fialova,
       Hneda,
       Ruzova,
       Zelena,
       Zlata,
       Oranzova,
       Bila,
       Sediva,
       Cerna,
       Modra,
       VojenskaZelena};
            String[] polePouzitychBarevVTextovychHodnotach = {
       "SvetleZelena",
       "Cervena",
       "TmaveModra",
       "Zluta",
       "SvetleModra",
       "Fialova",
       "Hneda",
       "Ruzova",
       "Zelena",
       "Zlata",
       "Oranzova",
       "Bila",
       "Sediva",
       "Cerna",
       "Modra",
       "VojenskaZelena"};

            for (int i = 0; i <= 15; i++)
            {
                PridejDalsiBarvuDoPolePouzitychBarevPokudJsouSplnenyPodminky(polePouzitychBarevVLogickychHodnotach[i], polePouzitychBarevVTextovychHodnotach[i]);
            }
        }
        private void PridejDalsiBarvuDoPolePouzitychBarevPokudJsouSplnenyPodminky(bool Barva, String TextBarvy)
        { if (Barva) { PolePouzitychBarev[IndexPolePouzitychBarev] = TextBarvy; IndexPolePouzitychBarev++; } }
        private bool NaplnSeznamPouzitychTypuMicuDanymBallsmPokudJsouSplnenyPodminky(bool DuhoveBalls, bool ZdvojnasobujiciBalls)
        {
            if (DuhoveBalls) { VlozNKratDoSeznamuPouzitychTypuMicuDanyText(5, "Duhove"); }
            for (int i = 0; i <= IndexPolePouzitychBarev - 1; i++)

            {
                VlozNKratDoSeznamuPouzitychTypuMicuDanyText(10, PolePouzitychBarev[i]);
                if (ZdvojnasobujiciBalls) { VlozNKratDoSeznamuPouzitychTypuMicuDanyText(1, String.Concat("Zdvojnasobujici", PolePouzitychBarev[i])); }
            }
            return true;

        }
        private void VlozNKratDoSeznamuPouzitychTypuMicuDanyText(int NKrat, String DanyText)
        {
            for (int i = 1; i <= NKrat; i++)
            {
                PouziteTypyMicu.Add(DanyText);
            };
        }

        public Ball VygenerujNovyMic()
        {
            String PouzityTypBalls = PouziteTypyMicu[RandomNumberGenerator.VratNahodneCislo(0, PouziteTypyMicu.Count())];
            Ball novyMic= NastavMicDleTypu(PouzityTypBalls);

             return novyMic;
        }
        private Ball NastavMicDleTypu(String PouzityTypBalls)
        {
            Ball novyMic;
            if (PouzityTypBalls.Contains("Duhove"))
            {
                novyMic = new Ball(true);
            } else
            {
                if (PouzityTypBalls.Contains("Zdvojnasobujici"))
                {
                    String PouzityTypBallsBezZdvojnasobuji = PouzityTypBalls.Substring(15);
                    novyMic = new Ball(PouzityTypBallsBezZdvojnasobuji, true);
                }
                else
                {
                    novyMic = new Ball(PouzityTypBalls);
                }
            }
            
            novyMic.NastavTyp(PouzityTypBalls);
            return novyMic;
        }

    }
}