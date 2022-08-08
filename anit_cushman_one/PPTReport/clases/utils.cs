using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;

namespace PPTReport.clases
{
    public class utils
    {
        public String ObteneLetra(int numero)
        {
            switch (numero)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";

            }
            return "-";
        }
        public  String GeneraImagen(String url, String nombreArchivo, string ruta)
        {
            String rutaArchivo = "";
            try
            {

                using (WebClient cliente = new WebClient())
                {
                    Byte[] byteImage = cliente.DownloadData(url);
                    MemoryStream memoryStream = new MemoryStream(byteImage);
                    Image image = Image.FromStream(memoryStream);
                    image.Save(ruta + nombreArchivo + ".png");
                    rutaArchivo = ruta + nombreArchivo + ".png";

                }


            }
            catch (Exception e)
            {
                rutaArchivo = "";
            }
            return rutaArchivo;
        }

        public void EliminaArchivos(string ruta)
        {
            string[] files = System.IO.Directory.GetFiles(ruta);
            string fileName = "";
            string fileext = "";
            foreach (string s in files)
            {
                fileName = System.IO.Path.GetFileName(s);
                fileext = System.IO.Path.GetExtension(s);
                if (fileext == ".png")
                {
                    System.IO.File.Delete(s);
                }

            }
        }
        public void EliminaTodosArchivos(string ruta)
        {
            string[] files = System.IO.Directory.GetFiles(ruta);
            string fileName = "";
            string fileext = "";
            foreach (string s in files)
            {
                fileName = System.IO.Path.GetFileName(s);
                fileext = System.IO.Path.GetExtension(s);
               
                System.IO.File.Delete(s);
                

            }
        }

    }
}
