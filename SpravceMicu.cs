using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Míče
{
    class SpravceMicu
    {
        List<String> PouziteTypyMicu = new List<String>();// Zde jsou registrovány všechny použité typy míčů, budou se generovat pouze míče, jejichž typy jsou v tomto registru.
        private String[] PolePouzitychBarev = new String [16];
        private int IndexPolePouzitychBarev = 0;
        public Queue<Mic> DalsiMice = new Queue<Mic>();
        public Mic DalsiMic1 = null;
        public Mic DalsiMic2 = null;
        public Mic DalsiMic3 = null;
        public SpravceMicu(
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
       bool DuhoveMice,
       bool ZdvojnasobujiciMice,
       Hra hra)
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
            NaplnSeznamPouzitychTypuMicuDanymMicemPokudJsouSplnenyPodminky(DuhoveMice,ZdvojnasobujiciMice);
            DalsiMic1 = VygenerujNovyMic();
            DalsiMic2 = VygenerujNovyMic();
            DalsiMic3 = VygenerujNovyMic();
            DalsiMice.Enqueue(DalsiMic1);
            DalsiMice.Enqueue(DalsiMic2);
            DalsiMice.Enqueue(DalsiMic3);

            
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
        private bool NaplnSeznamPouzitychTypuMicuDanymMicemPokudJsouSplnenyPodminky(bool DuhoveMice, bool ZdvojnasobujiciMice)
        {
            if (DuhoveMice) { VlozNKratDoSeznamuPouzitychTypuMicuDanyText(5, "Duhove"); }
            for (int i = 0; i <= IndexPolePouzitychBarev - 1; i++)

            {
                VlozNKratDoSeznamuPouzitychTypuMicuDanyText(10, PolePouzitychBarev[i]);
                if (ZdvojnasobujiciMice) { VlozNKratDoSeznamuPouzitychTypuMicuDanyText(1, String.Concat("Zdvojnasobujici", PolePouzitychBarev[i])); }
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

        public Mic VygenerujNovyMic()
        {
            String PouzityTypMice = PouziteTypyMicu[GeneratorNahodnychCisel.VratNahodneCislo(0, PouziteTypyMicu.Count())];
            Mic novyMic= NastavMicDleTypu(PouzityTypMice);

             return novyMic;
        }
        private Mic NastavMicDleTypu(String PouzityTypMice)
        {
            Mic novyMic;
            if (PouzityTypMice.Contains("Duhove"))
            {
                novyMic = new Mic(true);
            } else
            {
                if (PouzityTypMice.Contains("Zdvojnasobujici"))
                {
                    String PouzityTypMiceBezZdvojnasobuji = PouzityTypMice.Substring(15);
                    novyMic = new Mic(PouzityTypMiceBezZdvojnasobuji, true);
                }
                else
                {
                    novyMic = new Mic(PouzityTypMice);
                }
            }
            
            novyMic.NastavTyp(PouzityTypMice);
            return novyMic;
        }

    }
}