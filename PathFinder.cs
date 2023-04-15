using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Circles
{
    public class PathFinder
    {
        Cell AktivniPoleOdkud;
        Cell AktivniPoleKam;
        private Queue<NodeForPathFinder> FrontaPredTransformaci = new Queue<NodeForPathFinder>();// Vytvoří se fronta do které se vkládají uzle před transformací. Dále pouze jako horní fronta.
        private Queue<NodeForPathFinder> FrontaPoTransformaci = new Queue<NodeForPathFinder>();// Vytvoří se fronta do které se vkládají uzle po transformaci. Dále pouze jako dolní fronta.
        List<Cell> NavstivenaPole = new List<Cell>();
        private Stack<Cell> ZasobnikPoliOdkudKam = new Stack<Cell>();
        private NodeForPathFinder AktualniUzelHledaceCesty;
        private NodeForPathFinder PokudCestaExistujeJejiPosledniUzelJeZdeUzelHledaceCesty=null;
        Game hra = null;
        private Stack<Cell> ZasobnikPoliKtereUzNemajiBytAktivni = new Stack<Cell>();//Zde se dočasně ukládají pole, která budou při dalším kroku mít nastavené pozadí na normální.
        Cell ZkoumanePole= null;

        bool NaslaSeJizCesta = false;

        public PathFinder(Cell AktivniPoleOdkud, Cell AktivniPoleKam, Game hra, Stack<Cell> ZasobnikPoliKtereUzNemajiBytAktivni)
        {
            this.hra = hra;
            this.ZasobnikPoliKtereUzNemajiBytAktivni = ZasobnikPoliKtereUzNemajiBytAktivni;
            this.AktivniPoleOdkud = AktivniPoleOdkud;
            this.AktivniPoleKam = AktivniPoleKam;
            ZasobnikPoliOdkudKam.Push(AktivniPoleOdkud);
            ZasobnikPoliOdkudKam.Push(AktivniPoleKam);
        }
        
        public Stack<Cell> VratZasobnikPoliOdkudKam()
        {return this.ZasobnikPoliOdkudKam; }
        public bool Hledej()
        {
            NaslaSeJizCesta = false;
            AktualniUzelHledaceCesty = new NodeForPathFinder(null, AktivniPoleOdkud);// Vytvořil jsem úplně první uzel hledače cesty, který má v sobě odkaz na pole, z kterého chceme přesouvat míč.
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
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().getTopCell(); break;
                case "vpravo":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().getRightCell(); break;
                case "dole":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().getBottomCell(); break;
                case "vlevo":
                    ZkoumanePole = AktualniUzelHledaceCesty.VratPoleUzlu().getLeftCell(); break;

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
            if ((ZkoumanePole != null) && (ZkoumanePole.isEmpty()) && (!(NavstivenaPole.Contains(ZkoumanePole))))// Pokud pole nahoře je prázdné, nebylo již navštívené a existuje, tak potom se vytvoří nový uzel s tímto polem v dolní frontě a toto pole se přidá do seznamu již navštívených polí.
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