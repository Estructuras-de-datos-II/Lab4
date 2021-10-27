using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Encryptor;


namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        [HttpPost]
        [Route("ZigZag")]
        public IActionResult PostZigZag([FromForm] IFormFile[] files, [FromRoute] string nombre, [FromForm] string key)
        {
            byte[] buffer = new byte[0];
            byte[] bufferfinal = new byte[0];
            var mahByteArray = new List<byte[]>();
            int llave = Convert.ToInt32(key);
            String name = nombre;
            Encryptator cifrar = new ZigZagEncryptator();
            IFormFile file = files[0];
            string nombrearchiv = file.FileName;
            string nombrearchivofinal = nombrearchiv.Split(".").First();

            if (file == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(file.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Convert.ToInt32(key) == 0)
            {
                return BadRequest(new string[] { "El valor -Niveles- es inválido" });
            }
            else
            {
                using (MemoryStream archivotexto = new MemoryStream())

                    try
                    {
                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = cifrar.encrypt(archivotexto.ToArray(), llave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".zz");
                    }
                    catch (Exception)
                    {
                        return StatusCode(500);
                    }
                return Ok();
            }
        }
        [HttpPost]
        [Route("ruta")]
        public IActionResult PostRuta([FromForm] IFormFile[] files, [FromRoute] string nombre, [FromForm] string key)
        {
            byte[] buffer = new byte[0];
            byte[] bufferfinal = new byte[0];
            var mahByteArray = new List<byte[]>();
            int llave = Convert.ToInt32(key);
            String name = nombre;
            Encryptator cifrar = new RutaEncryptator ();
            IFormFile file = files[0];
            string nombrearchiv = file.FileName;
            string nombrearchivofinal = nombrearchiv.Split(".").First();

            if (file == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(file.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Convert.ToInt32(key) == 0)
            {
                return BadRequest(new string[] { "El valor -Niveles- es inválido" });
            }
            else
            {
                using (MemoryStream archivotexto = new MemoryStream())

                    try
                    {

                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = cifrar.encrypt(archivotexto.ToArray(), llave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".rt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e  + "\n\n");
                        return StatusCode(500);
                    }
                return Ok();
            }
        }

        [HttpPost]
        [Route("Cesar")]
        public IActionResult PostCesar([FromForm] List<IFormFile> files, [FromForm] string key)
        {
            byte[] buffer = new byte[0];
            byte[] bufferfinal = new byte[0];
            var mahByteArray = new List<byte[]>();
            byte[] bytellave = Encoding.ASCII.GetBytes(key);
            Encryptator cifrarcesar = new CesarEncrypt();
            IFormFile file = files[0];
            string nombrearchiv = file.FileName;
            string nombrearchivofinal = nombrearchiv.Split(".").First();

            if (file == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(file.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
           
            else
            {
                using (MemoryStream archivotexto = new MemoryStream())
                    
                    try
                    {
                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = cifrarcesar.encrypt(archivotexto.ToArray(), bytellave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".csr");
                    }
                    catch (Exception)
                    {
                        return StatusCode(500);
                    }
                return Ok();
            }
        }

        public void generarArchivo_txt(byte[] datos, string name, string extension)
        {
            string fileName = "../../Archivos/" + name + extension;
            Console.WriteLine(datos.Length + "\n\n\n");
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                
                fileStream.Write(datos, 0, datos.Length);
            }

        }

    }
}