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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace ConcretoArmado
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Declarando variáveis públicas
        public double es, esl, fyd, tsl;
        public double aas, asl;

        private void TxtDl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double fck, fyk, gamac, gamas, gamaf, bduct;
            double b, d, h, dl, delta, amk;
            double alamb, alfac, eu, qlim, ami, amilim;
            double fcd, tcd, amd;
            double qsi, qsia, romin, asmin, a;

            //
            //
            //Esta subrotina corresponde ao botão CALCULAR.
            //
            //
            //Dimensionamento de seções retangulares à flexão normal simples
            //
            //ENTRADA DE DADOS
            //
            //Os dados são lidos das caixas de texto do formulário
            //
            //Resistência característica à compressão do concreto em MPa
            //Substituindo a vírgula por ponto
            fck = Convert.ToDouble(txtFck.Text.Replace(",","."), CultureInfo.InvariantCulture);
            //
            //Tensão de escoamento característica do aço em MPa
            fyk = Convert.ToDouble(txtFyk.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Módulo de elasticidade do aço em GPa
            es = Convert.ToDouble(txtEs.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Coeficientes parciais de segurança:
            //para o concreto
            gamac = Convert.ToDouble(txtgamac.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //para o aço
            gamas = Convert.ToDouble(txtGamas.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //para o momento fletor
            gamaf = Convert.ToDouble(txtGamaf.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Coeficiente beta de redistribuição de momentos
            bduct = Convert.ToDouble(txtBeta.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Largura da seção transversal
            b = Convert.ToDouble(txtB.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Altura da seção transversal
            h = Convert.ToDouble(txtH.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Altura útil
            d = Convert.ToDouble(txtD.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Parâmetro d'
            dl = Convert.ToDouble(txtDl.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //Momento fletor de serviço em kNm
            amk = Convert.ToDouble(txtAMK.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            //
            //
            //FIM DA ENTRADA DE DADOS
            //
            //INÍCIO DOS CÁLCULOS
            //
            //
            //Parâmetros do diagrama retangular
            if (fck <= 50)
            {
                alamb = 0.8;
                alfac = 0.85;
                eu = 3.5;
                qlim = 0.8 * bduct - 0.35;
            }
            else
            {
                alamb = 0.8 - (fck - 50) / 400;
                alfac = 0.85 * (1 - (fck - 50) / 200);
                a = (90 - fck) / 100;
                eu = 2.6 + 35 * Math.Pow(a, 4);
                qlim = 0.8 * bduct - 0.45;
            }
            //
            //Conversão de unidades: transformando para kN e cm
            amk = 100 * amk;
            fck = fck / 10;
            fyk = fyk / 10;
            es = 100 * es;
            //
            //Resistências de cálculo
            fcd = fck / gamac;
            tcd = alfac * fcd;
            fyd = fyk / gamas;
            amd = gamaf * amk;
            //
            //Parâmetro geométrico
            delta = dl / d;
            //
            //Momento limite
            amilim = alamb * qlim * (1 - 0.5 * alamb * qlim);
            // 
            //Momento reduzido solicitante
            ami = amd / (b * d * d * tcd);
            //
            if (ami <= amilim)
            {
                //Armadura simples
                qsi = (1 - Math.Sqrt(1 - 2 * ami)) / alamb;
                aas = alamb * qsi * b * d * tcd / fyd;
                asl = 0;
            }
            if (ami > amilim)
            {
                //Armadura dupla
                //
                //Evitando armadura dupla no domínio 2
                qsia = eu / (eu + 10);
                if (qlim < qsia)
                {
                    //
                    // Está resultando armadura dupla no domínio 2. 
                    // Colocar mensagem para o usuário aumentar as dimensões da seção transversal e parar o processamento
                    //
                    MessageBox.Show("Resultou armadura dupla no domínio 2. Aumente as dimensões da seção transversal");
                    return;
                }
                //
                // Eliminando o caso em que qlim<delta
                // Se isto ocorrer, a armadura de compressão estará tracionada
                //
                if (qlim <= delta)
                {
                    //
                    // Colocar mensagem para o usuário aumentar as dimensões da seção transversal e parar o processamento
                    //
                    MessageBox.Show("Aumente as dimensões da seção transversal");
                    return;
                }
                //
                //Deformação da armadura de compressão
                esl = eu * (qlim - delta) / qlim;
                esl = esl / 1000;
                //  Tensão na armadura de compressão
                //  Chamar Sub-rotina
                Tensao();
                asl = (ami - amilim) * b * d * tcd / ((1 - delta) * tsl);
                aas = (alamb * qlim + (ami - amilim) / (1 - delta)) * b * d * tcd / fyd;
            }
            //
            //Armadura mínima
            a = 2.0 / 3.0;
            fck = 10 * fck;
            fyd = 10 * fyd;
            if (fck <= 50)
            {
                romin = 0.078 * Math.Pow(fck, a) / fyd;
            }
            else
            {
                romin = 0.5512 * Math.Log(1 + 0.11 * fck) / fyd;
            }
            if (romin < 0.0015)
            {
                romin = 0.0015;
            }
            //
            asmin = romin * b * h;
            if (aas < asmin)
            {
                aas = asmin;
            }
            //
            //Convertendo a saída para duas casas decimais
            //
            decimal saida1 = Decimal.Round(Convert.ToDecimal(aas), 2);
            decimal saida2 = Decimal.Round(Convert.ToDecimal(asl), 2);
            //
            //MOSTRAR O RESULTADO
            //Área da armadura tracionada: aas
            //Área da armadura comprimida: asl
            //
            txtAAS.Text = Convert.ToString(saida1);
            txtaal.Text = Convert.ToString(saida2);

        }

        private void Tensao()
        {
            double ess, eyd;
            //
            //Calcula a tensão no aço
            //es = módulo de elasticidade do aço em kN/cm2
            //esl = deformação de entrada
            //fyd = tensão de escoamento de cálculo em kN/cm2
            //tsl = tensão de saída em kN/cm2
            //
            //Trabalhando com deformação positiva
            ess = Math.Abs(esl);
            eyd = fyd / es;
            if (ess < eyd)
            {
                tsl = es * ess;
            }
            else
            {
                tsl = fyd;
            }
            //Trocando o sinal se necessário
            if (esl < 0)
            {
                tsl = -tsl;
            }
        }
    }
}