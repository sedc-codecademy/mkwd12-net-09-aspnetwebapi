

try
{
    using(HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync("http://localhost:5062/api/Test/testUser").Result;
        string responseBodyContent = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine(responseBodyContent);
    }

}catch(Exception ex)
{
    Console.WriteLine("An error occured");
    Console.WriteLine(ex.Message);
}