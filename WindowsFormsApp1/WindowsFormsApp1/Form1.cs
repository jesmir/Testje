using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmRekenMachine : Form
    {
        public frmRekenMachine()
        {
            InitializeComponent();
        }

        public enum RekenActie
        {
            GeenActie = -1,
            Telop = 0,
            TrekAf = 1,
            Deel = 3,
            Vermenigvuldig = 4,
            VeranderTeken = 5,
            MachtsVerhef = 6,
            WortelTrek = 7,
            Is = 8
        }

        public bool NieuweInput { get; set; } = true;
        public string HuidigeTekst { get; set; } = "";
        public string HuidigeFormule { get; set; } = "";
        public bool AanHetRekenen { get; set; } = false;
        public double HuidigResultaat { get; set; } = 0;
        public RekenActie LaatsteActie { get; set; } = RekenActie.GeenActie;
        public RekenActie HuidigeActie { get; set; } = RekenActie.GeenActie;
        public double HuidigeInput { get; set; } = 0;

        private void frmRekenMachine_Load(object sender, EventArgs e)
        {
            lblFormule.Text = "";
            lblInput.Text = "";
        }

        private void Herbegin()
        {
            NieuweInput = true;
            HuidigeTekst = "";
            HuidigeFormule = "";
            AanHetRekenen = false;
            HuidigResultaat = 0;
            LaatsteActie = RekenActie.GeenActie;
            HuidigeActie = RekenActie.GeenActie;
            HuidigeInput = 0;
        }

        private string ConverteerActie(RekenActie aActie)
        {
            string result = "";
            switch (aActie)
            {
                case RekenActie.GeenActie:
                    break;
                case RekenActie.Telop:
                    result = "+";
                    break;
                case RekenActie.TrekAf:
                    result = "-";
                    break;
                case RekenActie.Deel:
                    result = ":";
                    break;
                case RekenActie.Vermenigvuldig:
                    result = "x";
                    break;
                case RekenActie.VeranderTeken:
                    break;
                case RekenActie.MachtsVerhef:
                    break;
                case RekenActie.WortelTrek:
                    break;
                case RekenActie.Is:
                    result = "=";
                    break;
                default:
                    break;
            }
            return result;
        }

        private void Bereken()
        {
            double input = 0;
            double.TryParse(HuidigeTekst, out input);
            HuidigeInput = input;
            HuidigeFormule = HuidigeFormule + " " + HuidigeTekst + " " + ConverteerActie(LaatsteActie);
            double temp = 0;
            switch (HuidigeActie)
            {
                case RekenActie.GeenActie:
                    HuidigResultaat = HuidigeInput;
                    break;
                case RekenActie.Telop:                    
                    HuidigResultaat = TelOp(HuidigResultaat, HuidigeInput);
                    break;
                case RekenActie.TrekAf:
                    HuidigResultaat = TrekAf(HuidigResultaat, HuidigeInput);
                    break;
                case RekenActie.Deel:
                    Deel(HuidigResultaat, HuidigeInput, out temp);
                    HuidigResultaat = temp;
                    break;
                case RekenActie.Vermenigvuldig:
                    HuidigResultaat = Vermenigvuldig(HuidigResultaat, HuidigeInput);
                    break;
                case RekenActie.VeranderTeken:
                    break;
                case RekenActie.MachtsVerhef:
                    HuidigResultaat = MachtsVerhef(HuidigResultaat, HuidigeInput);
                    break;
                case RekenActie.WortelTrek:                    
                    WortelTrek(HuidigResultaat, (int)HuidigeInput, out temp);
                    HuidigResultaat = temp;
                    break;
                case RekenActie.Is:
                    break;
                default:
                    break;
            }

            ToonHuidigeFormule();
            ToonResultaat();
            NieuweInput = true;
        }


        private void ToonResultaat()
        {
            lblInput.Text = HuidigResultaat.ToString();
        }

        private void ToonHuidigeFormule()
        {
                lblFormule.Text = HuidigeFormule;
        }

        private void btnDeel_Click(object sender, EventArgs e)
        {
            AanHetRekenen = true;
            HuidigeActie = LaatsteActie;
            LaatsteActie = RekenActie.Deel;
            Bereken();
        }

        private void btnMaal_Click(object sender, EventArgs e)
        {
            AanHetRekenen = true;
            HuidigeActie = LaatsteActie;
            LaatsteActie = RekenActie.Vermenigvuldig;
            Bereken();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            AanHetRekenen = true;
            HuidigeActie = LaatsteActie;
            LaatsteActie = RekenActie.TrekAf;
            Bereken();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            AanHetRekenen = true;
            HuidigeActie = LaatsteActie;
            LaatsteActie = RekenActie.Telop;
            Bereken();
        }

        private void btnIs_Click(object sender, EventArgs e)
        {
            HuidigeActie = LaatsteActie;
            LaatsteActie = RekenActie.Is;
            Bereken();
            AanHetRekenen = false;
        }

        private void GetalInput(string aGetal)
        {
            if (NieuweInput)
            {
                HuidigeTekst = aGetal;
            }
            else
            {
                HuidigeTekst = HuidigeTekst + aGetal;
            }
            lblInput.Text = HuidigeTekst;
            NieuweInput = false;
        }

        private void btnNul_Click(object sender, EventArgs e)
        {
            GetalInput("0");
        }
               
        private void btnEen_Click(object sender, EventArgs e)
        {
            GetalInput("1");
        }

        private void btnTwee_Click(object sender, EventArgs e)
        {
            GetalInput("2");
        }

        private void btnDrie_Click(object sender, EventArgs e)
        {
            GetalInput("3");
        }

        private void btnVier_Click(object sender, EventArgs e)
        {
            GetalInput("4");
        }

        private void btnVijf_Click(object sender, EventArgs e)
        {
            GetalInput("5");
        }

        private void btnZes_Click(object sender, EventArgs e)
        {
            GetalInput("6");
        }

        private void btnZeven_Click(object sender, EventArgs e)
        {
            GetalInput("7");
        }

        private void btnAcht_Click(object sender, EventArgs e)
        {
            GetalInput("8");
        }

        private void btnNegen_Click(object sender, EventArgs e)
        {
            GetalInput("9");
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            if (NieuweInput)
            {
                if (!HuidigeTekst.Contains(","))
                {
                    HuidigeTekst =  "0,";
                }
            }
            else
            {
                if (!HuidigeTekst.Contains(","))
                {
                    HuidigeTekst = HuidigeTekst + ",";
                }
            }            
            lblInput.Text = HuidigeTekst;
        }

        private void btnWisselTeken_Click(object sender, EventArgs e)
        {

        }







        private double TelOp(double a, double b)
        {
            return a + b;
            //Math.fac
        }

        private double TrekAf(double a, double b)
        {
            return a - b;
        }

        private double Vermenigvuldig(double a, double b)
        {
            return a * b;
        }

        private bool Deel(double a, double b, out double aResultaat)
        {
            //double result = 0;
            //double rest = Math.DivRem(a, b, out result);
            //return result;
            aResultaat = 0;
            if (b == 0) return false;
            aResultaat =  a / b;
            return true;
        }

        private double MachtsVerhef(double aGetal, double aMacht)
        {
            return Math.Pow(aGetal, aMacht);
        }

        private bool WortelTrek(double aGetal, int aWortel, out double aResultaat)
        {
            aResultaat = 0;
            double wortel = aWortel;
            //double rest = Math.DivRem(1, aWortel, out wortel);
            bool delingGelukt = Deel(1, aWortel, out wortel);
            if (delingGelukt)
            {
                aResultaat = Math.Pow(aGetal, wortel);
                return true;
            }
            else
            {
                return false;
            }
            //return Math.Pow(A, 1.0 / N);            
        }
    }
}
