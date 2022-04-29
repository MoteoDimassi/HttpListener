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
            Console.WriteLine(ClientInformation(context));
            
            HttpListenerRequest request = context.Request;

            StreamReader sr = new StreamReader(request.InputStream);
            Console.WriteLine("data" + sr.ReadLine());

            HttpListenerResponse response = context.Response;
            string responseString;
            using (StreamReader reader = new StreamReader(@"C:\Users\d.djioev\Desktop\tests\www\index.html"))
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
    public static string ClientInformation(HttpListenerContext context)
    {
        System.Security.Principal.IPrincipal? user = context.User;
        if (user == null)
        {
            return "user is null";
        }
        System.Security.Principal.IIdentity? id = user.Identity;
        if (id == null)
        {
            return "Client authentication is not enabled for this Web server.";
        }

        string display;
        if (id.IsAuthenticated)
        {
            display = String.Format("{0} was authenticated using {1}", id.Name,
                id.AuthenticationType);
        }
        else
        {
            display = String.Format("{0} was not authenticated", id.Name);
        }
        return display;
    }
}


