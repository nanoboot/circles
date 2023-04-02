using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace Míče
{
    class OdpalovacMicu
    {
        private string TvarSkupinyMicuKteraExploduje;
        private int MinimalniDelkaLinky;
        private Stack<Pole> ZasobnikOdpalenychMicu = new Stack<Pole>();
        private Pole PoleKeKontrole = null;
        String jakouHledamBarvu;
        bool nalezenTvar = false;
        Pole aktualniPole;
        public OdpalovacMicu(String TvarSkupinyMicuKteraExploduje,int MinimalniDelkaLinky)
        { this.TvarSkupinyMicuKteraExploduje = TvarSkupinyMicuKteraExploduje;
            this.MinimalniDelkaLinky = MinimalniDelkaLinky;
        }
        public Stack<Pole> ZkontrolujAPripadneOdpal(Pole PoleKeKontrole)
        {
            this.PoleKeKontrole = PoleKeKontrole;
            aktualniPole=PoleKeKontrole;
            jakouHledamBarvu = PoleKeKontrole.VratMicANeodstranujHo().VratBarvu();
            nalezenTvar = false;
            switch (this.TvarSkupinyMicuKteraExploduje)
            {
                case "linka": OdpalTvarLinka(); break;
                case "ctverec": OdpalTvarCtverec(); break;
                case "krouzek": OdpalTvarKrouzek(); break;
                default: { }; break;
            }
            return this.ZasobnikOdpalenychMicu;//
        }
        private void OdpalTvarLinka()
        {
            String[] pozicePole = { "svisla", "vodorovna", "sikmazleva", "sikmazprava" };
            foreach (String pozice in pozicePole)
            { if (nalezenTvar) { break; } else { ZkontrolujPoziciLinky(pozice); } }

        }
        private void ZkontrolujPoziciLinky(String Pozice)
        {
            {
                aktualniPole = PoleKeKontrole;
                while ((aktualniPole != null) && (!aktualniPole.JePrazdne()) && (aktualniPole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu)))
                {
                    ZasobnikOdpalenychMicu.Push(aktualniPole);
                    switch (Pozice)
                    {
                        case "svisla":
                            aktualniPole = aktualniPole.VratPoleNahore(); break;
                        case "vodorovna":
                            aktualniPole = aktualniPole.VratPoleVlevo(); break;
                        case "sikmazleva":
                            aktualniPole = aktualniPole.VratPoleSikmoVlevoNahore(); break;
                        case "sikmazprava":
                            aktualniPole = aktualniPole.VratPoleSikmoVpravoNahore(); break;
                            
                            } 
                };
                aktualniPole = PoleKeKontrole;
                switch (Pozice)
                {
                    case "svisla":
                        aktualniPole = aktualniPole.VratPoleDole(); break;
                    case "vodorovna":
                        aktualniPole = aktualniPole.VratPoleVpravo(); break;
                    case "sikmazleva":
                        aktualniPole = aktualniPole.VratPoleSikmoVpravoDole(); break;
                    case "sikmazprava":
                        aktualniPole = aktualniPole.VratPoleSikmoVlevoDole(); break;

                }
                
                while ((aktualniPole != null) && (!aktualniPole.JePrazdne()) && (aktualniPole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu)))
                {
                    ZasobnikOdpalenychMicu.Push(aktualniPole);
                    switch (Pozice)
                    {
                        case "svisla":
                            aktualniPole = aktualniPole.VratPoleDole(); break;
                        case "vodorovna":
                            aktualniPole = aktualniPole.VratPoleVpravo(); break;
                        case "sikmazleva":
                            aktualniPole = aktualniPole.VratPoleSikmoVpravoDole(); break;
                        case "sikmazprava":
                            aktualniPole = aktualniPole.VratPoleSikmoVlevoDole(); break;
                }
                };

            }
            if (ZasobnikOdpalenychMicu.Count >= MinimalniDelkaLinky) { nalezenTvar = true; } else { ZasobnikOdpalenychMicu.Clear(); };
        }
        
        private void OdpalTvarCtverec()
        {
            String[] pozicePole = { "radek1sloupec1", "radek1sloupec2", "radek2sloupec1", "radek2sloupec2" };
            foreach (String pozice in pozicePole)
            { if (nalezenTvar) { break; } else { ZkontrolujPoziciCtverce(pozice); } }
        }
        private void ZkontrolujPoziciCtverce(String Pozice)
        {
            aktualniPole = PoleKeKontrole;
            Pole prvniZkoumanePole = null;
            Pole druheZkoumanePole = null;
            Pole tretiZkoumanePole = null;
            switch (Pozice)
            {
                case "radek1sloupec1":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleVpravo();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVpravoDole();
                        tretiZkoumanePole = aktualniPole.VratPoleDole();
                    }; break;
                case "radek1sloupec2":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleVlevo();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVlevoDole();
                        tretiZkoumanePole = aktualniPole.VratPoleDole();
                    }; break;
                case "radek2sloupec1":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleNahore();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVpravoNahore();
                        tretiZkoumanePole = aktualniPole.VratPoleVpravo();
                    }; break;
                case "radek2sloupec2":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleSikmoVlevoNahore();
                        druheZkoumanePole = aktualniPole.VratPoleNahore();
                        tretiZkoumanePole = aktualniPole.VratPoleVlevo();
                    }; break;

            }
            if ((prvniZkoumanePole != null) && (druheZkoumanePole != null) && (tretiZkoumanePole != null))
            {

                if (
                    ((!prvniZkoumanePole.JePrazdne()) && (prvniZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu))) &&
                    ((!druheZkoumanePole.JePrazdne()) && (druheZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu))) &&
                    ((!tretiZkoumanePole.JePrazdne()) && (tretiZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu)))
                    )
                {
                    nalezenTvar = true;
                    ZasobnikOdpalenychMicu.Push(aktualniPole);
                    ZasobnikOdpalenychMicu.Push(prvniZkoumanePole);
                    ZasobnikOdpalenychMicu.Push(druheZkoumanePole);
                    ZasobnikOdpalenychMicu.Push(tretiZkoumanePole);
                }
                else { ZasobnikOdpalenychMicu.Clear(); };

                ;
            }
        }
        private void OdpalTvarKrouzek()
        {
            String[] pozicePole = { "radek1sloupec2", "radek2sloupec1", "radek2sloupec3", "radek3sloupec2" };
            foreach (String pozice in pozicePole)
            { if (nalezenTvar) { break; } else { ZkontrolujPoziciKrouzku(pozice); } }
        }
        private void ZkontrolujPoziciKrouzku(String Pozice)
        {
            aktualniPole = PoleKeKontrole;
            Pole prvniZkoumanePole = null;
            Pole druheZkoumanePole = null;
            Pole tretiZkoumanePole = null;
            Pole poleVedleTretihoZkoumanehoPole = null;

            switch (Pozice)
            {
                case "radek1sloupec2":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleSikmoVlevoDole();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVpravoDole();
                        poleVedleTretihoZkoumanehoPole = aktualniPole.VratPoleDole();
                        if (poleVedleTretihoZkoumanehoPole!=null) tretiZkoumanePole = poleVedleTretihoZkoumanehoPole.VratPoleDole();
                    }; break;
                case "radek2sloupec1":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleSikmoVpravoNahore();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVpravoDole();
                        poleVedleTretihoZkoumanehoPole = aktualniPole.VratPoleVpravo();
                        if (poleVedleTretihoZkoumanehoPole != null) tretiZkoumanePole = poleVedleTretihoZkoumanehoPole.VratPoleVpravo();
                    }; break;
                case "radek2sloupec3":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleSikmoVlevoNahore();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVlevoDole();
                        poleVedleTretihoZkoumanehoPole = aktualniPole.VratPoleVlevo();
                        if (poleVedleTretihoZkoumanehoPole != null) tretiZkoumanePole = poleVedleTretihoZkoumanehoPole.VratPoleVlevo();
                    }; break;
                case "radek3sloupec2":
                    {
                        prvniZkoumanePole = aktualniPole.VratPoleSikmoVlevoNahore();
                        druheZkoumanePole = aktualniPole.VratPoleSikmoVpravoNahore();
                        poleVedleTretihoZkoumanehoPole = aktualniPole.VratPoleNahore();
                        if (poleVedleTretihoZkoumanehoPole != null) tretiZkoumanePole = poleVedleTretihoZkoumanehoPole.VratPoleNahore();
                    }; break;
            }
            if ((prvniZkoumanePole != null) && (druheZkoumanePole != null) && (poleVedleTretihoZkoumanehoPole != null) && (tretiZkoumanePole != null))
            {

                if (
                    ((!prvniZkoumanePole.JePrazdne()) && (prvniZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu))) &&
                    ((!druheZkoumanePole.JePrazdne()) && (druheZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu))) &&
                    ((!tretiZkoumanePole.JePrazdne()) && (tretiZkoumanePole.VratMicANeodstranujHo().jeJehoBrava(jakouHledamBarvu)))
                    )
                {
                    nalezenTvar = true;
                    ZasobnikOdpalenychMicu.Push(aktualniPole);
                    ZasobnikOdpalenychMicu.Push(prvniZkoumanePole);
                    ZasobnikOdpalenychMicu.Push(druheZkoumanePole);
                    ZasobnikOdpalenychMicu.Push(tretiZkoumanePole);
                }
                else { ZasobnikOdpalenychMicu.Clear(); };

                ;
            }
        }
    }
}