namespace SemanticKernelDemo
{
    //using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.ChatCompletion;

    public class Tests
    {
        Kernel AzureKernel;
        Kernel LocalKernel;
        [SetUp]

        public void Setup()
        {

            LocalModelHandler handler = new LocalModelHandler(@"http://localhost:1234");
            HttpClient client = new HttpClient(handler);


          
            //var Builder = Kernel.CreateBuilder();
            //Builder.AddAzureOpenAIChatCompletion("Model", "Endpoint", "Key");
            //AzureKernel = Builder.Build();

            var BuilderLocal = Kernel.CreateBuilder();
            BuilderLocal.AddOpenAIChatCompletion("phi3", "api-key", httpClient: client);
            LocalKernel = BuilderLocal.Build();
        }

        [Test]
        public async Task WriteTacosRecipe()
        {
            string Prompt="Write a recipe for tacos al pastor";
            var Response=await LocalKernel.InvokePromptAsync(Prompt);
            Console.WriteLine(Response);

            Assert.Pass();
        }
    }
}