using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class MaQRController : Controller
    {
        // GET: MaQR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return View(BitmapToBytes(qrCodeImage));
        }
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public ActionResult GenerateFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateFile(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
            qrCodeData.SaveRawData("~/FileUpLoad/qrCoder/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);

            QRCodeData qrCodeData1 = new QRCodeData("~/FileUpLoad/qrCoder/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);
            QRCode qrCode = new QRCode(qrCodeData1);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return View(BitmapToBytes(qrCodeImage));
        }

        public ActionResult ViewFile()
        {
            List<KeyValuePair<string, Byte[]>> fileData = new List<KeyValuePair<string, byte[]>>();
            KeyValuePair<string, Byte[]> data;

            string[] files = Directory.GetFiles("~/FileUpLoad/qrCoder");
            foreach (string file in files)
            {
                QRCodeData qrCodeData = new QRCodeData(file, QRCodeData.Compression.Uncompressed);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                Byte[] byteData = BitmapToBytes(qrCodeImage);
                data = new KeyValuePair<string, Byte[]>(Path.GetFileName(file), byteData);
                fileData.Add(data);
            }

            return View(fileData);
        }
        public ActionResult TaoMaQR()
        {

            return View();
        }
    }
}