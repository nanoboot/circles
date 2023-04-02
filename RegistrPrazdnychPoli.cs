using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


namespace Míče
{
    class RegistrPrazdnychPoli
    {
        private UzelRegistruPrazdnychPoli PrvniUzel = null;
        private UzelRegistruPrazdnychPoli DocasnyUzel = null;
        private UzelRegistruPrazdnychPoli PredchoziDocasnyUzel = null;
        private int PocetUzlu = 0;
        private void PocetUzluPlusJedna()// Počet polí registru se zvýší o jedna.
        { PocetUzlu = ++PocetUzlu;
            //Zaznam.Zaznamenej("Počet uzlů byl navýšen na ", this.VratPocetUzlu().ToString());
        }
        private void PocetUzluMinusJedna()// Počet polí registru se sníží o jedna.
        { PocetUzlu = --PocetUzlu; }
        public int VratPocetUzlu()
        {
           
            return this.PocetUzlu; }

        public void VlozPole(Pole NovePole)//Vloží pole do registru.
        {

            if (PrvniUzel == null) { PrvniUzel = new UzelRegistruPrazdnychPoli(NovePole); }
            if (PrvniUzel != null) { DocasnyUzel = PrvniUzel;
                this.PrvniUzel = new UzelRegistruPrazdnychPoli(NovePole);
                this.PrvniUzel.NastavitDalsiUzel(DocasnyUzel);
            }
            PocetUzluPlusJedna();
        }
        public Pole VratPole(int Poradi)//Vrátí dané prázdné pole z registru a zároveň toto pole vymaže z registru.
        {
            if (Poradi <= VratPocetUzlu())
            {
                if (Poradi == 1)
                {
                    DocasnyUzel = this.PrvniUzel;
                    PrvniUzel = PrvniUzel.VratDalsiUzel();
                    PocetUzluMinusJedna();
                    return DocasnyUzel.VratPole();
                }
                if (Poradi > 1)
                {
                    DocasnyUzel = this.PrvniUzel;
                    
                    for (int i = 1; i < Poradi; i++)
                    {
                        PredchoziDocasnyUzel = DocasnyUzel;
                        DocasnyUzel = DocasnyUzel.VratDalsiUzel(); }
                    PredchoziDocasnyUzel.NastavitDalsiUzel(DocasnyUzel.VratDalsiUzel());
                    PocetUzluMinusJedna();
                    return DocasnyUzel.VratPole();
                }else return new Pole();// Zde nějak opravit.


            }
            else
            { /*return new Pole();*/ throw new ArgumentOutOfRangeException(); }// Zde nějak opravit.
        }
        public void OdstranPole(Pole novePlnePole)//Vrátí dané prázdné pole z registru a zároveň toto pole vymaže z registru.
        {
           DocasnyUzel = PrvniUzel;
            while((DocasnyUzel.VratDalsiUzel()!=null)&&(DocasnyUzel.VratPole()!= novePlnePole))
                { PredchoziDocasnyUzel = DocasnyUzel;
                DocasnyUzel = DocasnyUzel.VratDalsiUzel();
            }
            if (DocasnyUzel.VratPole()==novePlnePole)
            { PredchoziDocasnyUzel.NastavitDalsiUzel(DocasnyUzel.VratDalsiUzel());
                PocetUzluMinusJedna();
            }
        }
    }
}
