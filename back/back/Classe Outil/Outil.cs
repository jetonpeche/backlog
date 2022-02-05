using Microsoft.AspNetCore.StaticFiles;
using System.Text.RegularExpressions;

namespace back
{
    public static class Outil
    {
        public static string ProtectionXSS(string _text)
        {
            Regex regHtml = new Regex("<[^>]*>");
            return regHtml.Replace(_text, "");
        }

        public static string ProtectionImage(IFormFile _fichier, int _poidsMax, string[] _listeExtension)
        {
            string typeMime = "";

            var p = new FileExtensionContentTypeProvider();
            p.TryGetContentType(_fichier.FileName, out typeMime);

            if (_fichier.Length <= _poidsMax)
            {
                // verifie le type mime de l'image
                if (typeMime == "image/png")
                {
                    // verifie l'extention
                    if (_listeExtension.Contains(Path.GetExtension(_fichier.FileName)))
                    {
                        return "ok";
                    }
                    else
                    {
                        return "extension incorrect";
                    }
                }
                else
                {
                    return "Le format de l'image doit etre PNG";
                }
            }
            else
            {
                return "Le poids du fichier ne peut pas etre plus grand que: " + _poidsMax;
            }
        }
    }
}
