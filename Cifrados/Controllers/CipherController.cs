using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cifrados.Modelo;
using Cifrados.Cifrados;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        Encoding utf8 = Encoding.UTF8;
        [Route("ZigZag")]
        public ActionResult<IEnumerable<string>> PostZigZag([FromForm]Requisitos Tipos)//Comprobar mas errores posibles
        {

            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Niveles == 0)
            {
                return BadRequest(new string[] { "El valor -Niveles- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    ZigZag zigZag = new ZigZag();
                    using var archivotexto = new MemoryStream();
                    Tipos.File.CopyToAsync(archivotexto);
                    var textomientras = Encoding.UTF8.GetString(archivotexto.ToArray());
                    Byte[] stringbytes = utf8.GetBytes(textomientras);
                    string textoparacomprimir = "";
                    textoparacomprimir = Encoding.UTF8.GetString(stringbytes);
                    zigZag.TodoZigZag(thisFile,"Cifrar", Tipos.Niveles, textoparacomprimir);
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
        [Route("Cesar")]
        public ActionResult<IEnumerable<string>> PostCesar([FromForm]Requisitos Tipos)//Comprobar mas errores posibles
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Key == null || !(int.TryParse(Tipos.Key, out int Key)))
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    Cesar Cesar = new Cesar();
                    using var archivotexto = new MemoryStream();
                    Tipos.File.CopyToAsync(archivotexto);
                    var textomientras = Encoding.UTF8.GetString(archivotexto.ToArray());
                    Byte[] stringbytes = utf8.GetBytes(textomientras);
                    string textoparacomprimir = "";
                    textoparacomprimir = Encoding.UTF8.GetString(stringbytes);

                    //Archivo-Llave-Desifrado
                    Cesar.TodoCesar(thisFile, Key, "Cifrado", textoparacomprimir);
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
        
    }
}