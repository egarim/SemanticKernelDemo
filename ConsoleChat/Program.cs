using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace ConsoleChat;

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
class Program
{
    static async Task Main(string[] args)
    {
        LocalModelHandler handler = new LocalModelHandler(@"http://localhost:1234");
        HttpClient client = new HttpClient(handler);



        //var Builder = Kernel.CreateBuilder();
        //Builder.AddAzureOpenAIChatCompletion("Model", "Endpoint", "Key");
        //AzureKernel = Builder.Build();

        var BuilderLocal = Kernel.CreateBuilder();
        BuilderLocal.AddOpenAIChatCompletion("phi3", "api-key", httpClient: client);
        var LocalKernel = BuilderLocal.Build();
        var kernel = LocalKernel;

        string Prompt = "Write a recipe for tacos al pastor";
        //var Response=await LocalKernel.InvokePromptAsync(Prompt);
        //Console.WriteLine(Response);


// 4. Let's create a prompt - What our AI app does
        var prompt = @"
Chatbot can have a conversation with you about any topic.
It can give explicit information or say 'I don't know' if it doesn't have an answer.
 
User:{{$userInput}}
ChatBot:";

// 5. Create a Symentic Function - An AI function
        var chatFunction = kernel.CreateFunctionFromPrompt(prompt, 
            executionSettings: new OpenAIPromptExecutionSettings { MaxTokens = 2000, Temperature = 0.7, TopP = 0.5 });

// 6. Build the Arguments
        var arguments = new KernelArguments();

// 7. Get user Inputs
        Console.WriteLine("Hi, I am a chatBot, ask me anything!");
        var readUserInput = Console.ReadLine();
        arguments["userInput"] = readUserInput;

// 8. Start with a basic chat
        var bot_answer = await chatFunction.InvokeAsync(kernel, arguments);
        Console.Write(bot_answer);

    }


}