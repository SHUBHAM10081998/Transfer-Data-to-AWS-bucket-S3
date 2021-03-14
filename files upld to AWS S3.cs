        public class AmazonUploader
        {
            public bool sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
            {
                IAmazonS3 client = new AmazonS3Client(RegionEndpoint.APSouth1);
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    request.BucketName = bucketName;  
                }
                else
                {
                    request.BucketName = bucketName + @"/" + subDirectoryInBucket;
                }
                request.Key = fileNameInS3;  
                request.InputStream = localFilePath;
                utility.Upload(request); 
                return true;  
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {            
            string s = Server.MapPath("~/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            DateTime now = DateTime.Now;
            string curdt = now.ToString("ddMMMyyyy");
            DirectoryInfo d = new DirectoryInfo(@"D:\Shubham\"+curdt+"");
            FileInfo[] Files = d.GetFiles("*.WAV");
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = file.Name;
                byte[] byteArray = Encoding.ASCII.GetBytes(s);
                MemoryStream stream = new MemoryStream(byteArray);
                string name = Path.GetFileName(FileUpload1.FileName);
                string myBucketName = "reliablebss";
                string s3DirectoryName = "GOQQI";
                string s3FileName = @str;
                AmazonUploader myUploader = new AmazonUploader();
                a = myUploader.sendMyFileToS3(stream, myBucketName, s3DirectoryName, s3FileName);
            }
            if (a == true)
            {
                Response.Write("successfully uploaded");
            }
            else
                Response.Write("Error");
        }
    }
