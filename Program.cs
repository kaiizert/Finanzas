using System;
using System.IO;



partial class  Program

{
    
  static void Main()
    {
        
       
          
         
        

        string  Credentials = "Credentials.txt";
        if (!File.Exists(Credentials))
        {
            Console.WriteLine("Bienvenido, por favor ingrese su nombre de usuario:");
            string? Nombre = Console.ReadLine();
            Console.WriteLine("Un gusto conocerte!" + Nombre + ", por favor ingresa tu contraseña:");
            string? clave = Console.ReadLine();
            Console.WriteLine("Gracias por registrarte, " + Nombre + "!");
            using (StreamWriter sw = File.CreateText(Credentials))
            {
                sw.WriteLine(Nombre);
                sw.WriteLine(clave);
            }


        }

        else
        {
            using (StreamReader sr = File.OpenText(Credentials))
            {
                string? Nombre = sr.ReadLine();
                string? clave = sr.ReadLine();
                Console.WriteLine("Bienvenido,"+  Nombre + "Porfavor ingresa tu contraseña:" );
                string? inputClave = Console.ReadLine();
                if (inputClave == clave)
                {
                    Console.WriteLine("Iniciando sesion...");

                }
                else
                {
                    Console.WriteLine("Contraseña incorrecta, por favor intente de nuevo.");
                }
            }
                        
        }

        while (true)
        {
            Console.WriteLine("¿Qué operación deseas realizar? (ingresa el número correspondiente) ");
        }
        
    }
}



