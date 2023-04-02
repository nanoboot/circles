using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Míče
{
    public class HledacCesty
    {
        Pole AktivniPoleOdkud;
        Pole AktivniPoleKam;
        private Queue<UzelHledaceCesty> FrontaPredTransformaci = new Queue<UzelHledaceCesty>();// Vytvoří se fronta do které se vkládají uzle před transformací. Dále pouze jako horní fronta.
        private Queue<UzelHledaceCesty> FrontaPoTransformaci = new Queue<UzelHledaceCesty>();// Vytvoří se fronta do které se vkládají uzle po transformaci. Dále pouze jako dolní fronta.
        List<Pole> NavstivenaPole = new List<Pole>();
        private Stack<Pole> ZasobnikPoliOdkudKam = new Stack<Pole>();
        private UzelHledaceCesty AktualniUzelHledaceCesty;
        private UzelHledaceCesty PokudCestaExistujeJejiPosledniUzelJeZdeUzelHledaceCesty=null;
        Hra hra = null;
        private Stack<Pole> ZasobnikPoliKtereUzNemajiBytAktivni = new Stack<Pole>();//Zde se dočasně ukládají pole, která budou při dalším kroku mít nastavené pozadí na normální.
        Pole ZkoumanePole= null;

        bool NaslaSeJizCesta = false;

        public HledacCesty(Pole AktivniPoleOdkud, Pole AktivniPoleKam, Hra hra, Stack<Pole> ZasobnikPoliKtereUzNemajiBytAktivni)
        {
            this.hra = hra;
            this.ZasobnikPoliKtereUzNemajiBytAktivni = ZasobnikPoliKtereUzNemajiBytAktivni;
            this.AktivniPoleOdkud = AktivniPoleOdkud;
            this.AktivniPoleKam = AktivniPoleKam;
            ZasobnikPoliOdkudKam.Push(AktivniPoleOdkud);
            ZasobnikPoliOdkudKam.Push(AktivniPoleKam);
        }
        
        public Stack<Pole> VratZasobnikPoliOdkudKam()
        {return this.ZasobnikPoliOdkudKam; }
        public bool Hledej()
        {
            NaslaSeJizCesta = false;
            AktualniUzelHledaceCesty = new UzelHledaceCesty(null, AktivniPoleOdkud);// Vytvořil jsem úplně první uzel hledače cesty, který má v sobě odkaz na pole, z kterého chceme přesouvat míč.
            FrontaPredTransformaci.Enqueue(AktualniUzelHledaceCesty);//První uzel jsem vložil do fronty FrontaPredTransformaci
            while(!((FrontaPredTransformaci.Count == 0) && (FrontaPoTransformaci.Count == 0)))// Dokud fronty nejsou prázdné, je co transformovat.
            {
                // Vezme postupně všechny uzle z fronty FrontaPredTransformaci
                if (FrontaPredTransformaci.Count == 0)//Pokud horní fronta je prázdná.
                    PresunUzleZDolníFrontyDoHorniFronty();// Vše z dolní fronty se přesune do horní fronty.
                NaslaSeJizCesta = VezmiZHorniFrontyJedenUzelAZkoumejHo();
                if (NaslaSeJizCesta) { return true; };
                ;
            };
            
            return false; //To, že fronta došla až sem, znamená, že jsme nenašli cestu, proto vrátí false.
        }
        private void PresunUzleZDolníFrontyDoHorniFronty()
        {
            while (FrontaPoTransformaci.Count != 0)//Dokud dolní fronta není prázdná,
                FrontaPredTransformaci.Enqueue(FrontaPoTransformaci.Dequeue());// přesunuj uzle z dolní fronty do horní fronty.
        }
        private bool VezmiZHorniFrontyJedenUzelAZkoumejHo()
        {
            AktualniUzelHledaceCesty = FrontaPredTransformaci.Dequeue();//Vezme jeden uzel z horní fronty
                                                                        // Z horní fronty jsem vzal jeden uzel a nyní budu zkoumat, všechny možné cesty z tohoto uzlu.
            bool LogickaHodnota = false;
            String[] smerPole = { "nahore", "vpravo", "dole", "vlevo" };
            foreach (String smer in smerPole)
            {
                LogickaHodnota = ZkoumejZdaUzelZHorniFrontyJeUzelPoleKamChcemePresunoutMic(smer); if (LogickaHodnota) { return true; }
                else
                {
                    PokudPoleVDanemSmeruOdAktualnihoPoleSplnujePodminkyPresunHoDoDolniFrontyADoGenerickehoSeznamuNavstivenychPoli(smer);
                }
            }

            return false;
        }
        private bool ZkoumejZdaUzelZHorniFrontyJeUzelPoleKamChcemePresunoutMic(String Smer)
        {
            switch (Smer)
            {
                case "nahore":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().VratPoleNahore(); break;
                case "vpravo":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().VratPoleVpravo(); break;
                case "dole":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().VratPoleDole(); break;
                case "vlevo":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().VratPoleVlevo(); break;

            }
            if (ZkoumanePole == AktivniPoleKam)
            {
                this.PokudCestaExistujeJejiPosledniUzelJeZdeUzelHledaceCesty = AktualniUzelHledaceCesty.VytvorDite(ZkoumanePole);

                {
                    while (AktualniUzelHledaceCesty.VratRodice() != null)
                    {
                        ZasobnikPoliOdkudKam.Push(AktualniUzelHledaceCesty.VratPoleUzlu());
                        AktualniUzelHledaceCesty = AktualniUzelHledaceCesty.VratRodice();
                    }
                }

                return true;
            };// Pokud pole nahoře od pole aktuálního uzlu je pole, kam chceme přesunout míč, nalezli jsme cestu a tato metoda vrací true a končí, jinak se pokračuje. 
            return false;
        }
        private bool PokudPoleVDanemSmeruOdAktualnihoPoleSplnujePodminkyPresunHoDoDolniFrontyADoGenerickehoSeznamuNavstivenychPoli(String Smer)
        {
            if ((ZkoumanePole != null) && (ZkoumanePole.JePrazdne()) && (!(NavstivenaPole.Contains(ZkoumanePole))))// Pokud pole nahoře je prázdné, nebylo již navštívené a existuje, tak potom se vytvoří nový uzel s tímto polem v dolní frontě a toto pole se přidá do seznamu již navštívených polí.
            {
                FrontaPoTransformaci.Enqueue(AktualniUzelHledaceCesty.VytvorDite(ZkoumanePole));
                NavstivenaPole.Add(ZkoumanePole);

                //ZasobnikPoliKtereUzNemajiBytAktivni.Push(ZkoumanePole);
                //hra.VlozPrikaz((String.Concat("POLE ", ZkoumanePole.VratRadek(), " ", ZkoumanePole.VratSloupec(), " POZADI CERVENE")));
            return true;
            }
            else
            {
                return false;
            }
        }
    }
    }