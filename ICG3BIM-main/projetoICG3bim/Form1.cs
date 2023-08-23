using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoICG3bim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //-------------------- VARIAVEIS --------------------

        int coluna = 0;
        int linha = 0;
        bool verf = false;
        bool sobreposta = false;
        bool remove = false;
        Color cor;
        Bitmap imgCozinha = new Bitmap("C:\\Pictures\\Imagem_A.jpg");
        Bitmap imgPanela = new Bitmap("C:\\Pictures\\Panela.jpg");

        //-------------------- IMAGEM CINZA --------------------

        private void button1_Click(object sender, EventArgs e)
        {
            coluna = imgCozinha.Width; // O número colunas 
            linha = imgCozinha.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha); 
            if (verf == true)
            {
                imgCozinha = SobrepoeImagem();
            }

            cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    double r = imgCozinha.GetPixel(i, j).R;
                    double g = imgCozinha.GetPixel(i, j).G;
                    double b = imgCozinha.GetPixel(i, j).B;

                    double K = r * 0.3 + g * 0.59 + b * 0.11;

                    cor = Color.FromArgb((int)K, (int)K, (int)K);
                    imgnova.SetPixel(i, j, cor);

                }
            }
            pictureBox1.Image = imgnova;

            if(sobreposta == true)
                pictureBox4.Image = imgnova;

            imgnova.Save("TESTE3.jpg");
        }

        //---------------------------------------------------------------------

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*****************************************/
        }

        //---------------------------------------------------------------------

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*****************************************/
        }

        //-------------------- REMOVE FUNDO --------------------

        private void button2_Click(object sender, EventArgs e)
        {
            remove = true;
            Bitmap imgnova = tiraAmarelo();
            pictureBox2.Image = imgnova;
            imgnova.Save("TESTE3.jpg");
        }

        //---------------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = imgCozinha;
            pictureBox2.Image = imgPanela;

        }

        //-------------------- IMAGEM P/B --------------------

        private void button3_Click(object sender, EventArgs e)
        {
            coluna = imgCozinha.Width; // O número colunas 
            linha = imgCozinha.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha);
            if (verf == true)
            {
                imgCozinha = SobrepoeImagem();
            }
            cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    double r = imgCozinha.GetPixel(i, j).R;
                    double g = imgCozinha.GetPixel(i, j).G;
                    double b = imgCozinha.GetPixel(i, j).B;

                    double K = r * 0.3 + g * 0.59 + b * 0.11;

                    if (K >= 127)
                        K = 255;
                    else
                        K = 0;
                    
                    cor = Color.FromArgb((int)K, (int)K, (int)K);
                    imgnova.SetPixel(i, j, cor);

                }
            }

            imgnova.Save("TESTE2.jpg");

            if (sobreposta == true)
                pictureBox5.Image = imgnova;

            pictureBox1.Image = imgnova;
        }

        //---------------------------------------------------------------------

        private Bitmap tiraAmarelo()
        {
            int coluna = imgPanela.Width; // O número colunas 
            int linha = imgPanela.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha);
            Color cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    int r = imgPanela.GetPixel(i, j).R;
                    int g = imgPanela.GetPixel(i, j).G;
                    int b = imgPanela.GetPixel(i, j).B;

                    if ((r + g)/2 >= 220 && (r+g+b)/3 <= 230)
                    {
                        imgnova.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                    }
                    else
                    {
                        cor = Color.FromArgb(r, g, b);
                        imgnova.SetPixel(i, j, cor);
                    }

                }
            }
            return imgnova;
        }

        //---------------------------------------------------------------------

        private Bitmap SobrepoeImagem()
        {
            Bitmap panela = tiraAmarelo();
            Bitmap img_resultado = new Bitmap(imgCozinha);
            int cont1 = 150;

            for (int y = 0; y < panela.Width; y++)
            {
                cont1++;
                int cont2 = 0;
                for (int x = 0; x < panela.Height; x++)
                {
                    cont2++;
                    Color pixelSobre = panela.GetPixel(y, x);

                    // Se a cor do pixel da sobreposição não for transparente, aplicar sobreposição
                    if (pixelSobre.A != 0)
                    {
                        img_resultado.SetPixel(cont1, cont2, pixelSobre);
                    }
                }
            }
            return img_resultado;
            
        }

        //-------------------- SOBREPOR IMG ---------------------

        private void button4_Click(object sender, EventArgs e)
        {
            verf = true;
            sobreposta = true;
            if (remove == true) { 
                Bitmap img_resultado = SobrepoeImagem();
                pictureBox1.Image = img_resultado;
                pictureBox3.Image = img_resultado;
            }else
                MessageBox.Show("REMOVA O FUNDO!");

            
        }
    }
}
