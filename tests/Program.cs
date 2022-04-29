using System.Net;

class Program
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        Console.WriteLine("Listening...");
        while (true)
        {

            HttpListenerContext context = listener.GetContext();


            HttpListenerRequest request = context.Request;

            StreamReader sr = new StreamReader(request.InputStream);
            for (string line = null; line != null; line = sr.ReadLine())
                Console.WriteLine(line);

            HttpListenerResponse response = context.Response;
            string responseString;
            using (StreamReader reader = new StreamReader(@"..\..\..\www\index.html"))
            {
                responseString = reader.ReadToEnd();
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            //response.ContentLength64 = buffer.Length;
            //response.StatusCode = (int)HttpStatusCode.OK;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
        listener.Stop();

    }
    static void webControl()
    {

    }

}


