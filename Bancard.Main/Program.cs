using Bancard.API;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
BancardService bancardService = new BancardService();
var result = await bancardService.SingleBuy(543226, "PYG", "10330.00", "1033.00", "099VS ORO000046", "Ejemplo de Pago", "http://www.example.com/finish", "http://www.example.com/cancel");
//var result = await bancardService.Test();
//var result = await bancardService.GetItem(543226, "PYG", "10330.00", "1033.00", "099VS ORO000046", "Ejemplo de Pago", "http://www.example.com/finish", "http://www.example.com/cancel");

Console.WriteLine(result);
