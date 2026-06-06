using System;
using System.IO;



partial class  Program

{
    
  static void Main()
    {
        
        string  Credentials = "Credentials.txt";
        string Historial = "Historial.txt";
        string Finanzas = "Finanzas.txt";
        decimal balance = 0;
        if (!File.Exists(Historial))
        {
            File.Create(Historial).Close();
        }
        if (!File.Exists(Finanzas))
        {
            File.Create(Finanzas).Close();
        } 
         
        
  
        
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
            finanza();


        }

        else if (File.Exists(Credentials) && File.ReadAllText(Credentials).Trim() == "")
        {
            File.Delete(Credentials);
            Console.WriteLine("Los archivos estan corrompidos o vacios, por favor, vuelva a crear su cuenta");
            Main();
            return;

        }

        else
        {
            using (StreamReader sr = File.OpenText(Credentials))
            {
                string? Nombre = sr.ReadLine();
                string? clave = sr.ReadLine();
                Console.WriteLine("Bienvenido,"+  Nombre  + "Porfavor ingresa tu contraseña:" );
                string? inputClave = Console.ReadLine();
                if (inputClave == clave)
                {
                    Console.WriteLine("Iniciando sesion...");
                    finanza();
                    
        
                }
                else
                {
                    while (inputClave != clave)
                    {
                        Console.WriteLine("Contraseña incorrecta, por favor intente de nuevo.");
                        inputClave = Console.ReadLine();
                        if (inputClave == clave)
                        {
                            Console.WriteLine("Iniciando sesion...");
                            finanza();
                            break;
                        }
                    }
            
                }
            }
                        
        }


       void finanza()
        {

            while  (true)
        {
            Console.WriteLine("¿Qué operación deseas realizar? (ingresa el número correspondiente) \n1. registrar ingreso \n2. registrar Gasto \n3. mostrar balance \n4. ver historial  \n5. salir");
            string? opcion = Console.ReadLine();
            switch (opcion)
            {
                
                case "1":
                    Console.WriteLine("Ingrese el monto del ingreso:");
                     string? ingreso = Console.ReadLine();
                     if (decimal.TryParse(ingreso, out decimal ingresoDecimal))
                    {
                        balance += ingresoDecimal;
                        using (StreamWriter sw = File.AppendText(Finanzas ))
                        {
                        
                            sw.WriteLine("Balance actual: " + balance);
                        }
                        using (StreamWriter sw = File.AppendText(Historial))
                        {
                            sw.WriteLine("Ingreso: " + ingresoDecimal + " - " + DateTime.Now);
                        }

                        
                    }
                    else
                    {
                        Console.WriteLine("Monto no válido, por favor intente de nuevo.");
                    }                     
                    break;
                case "2":
                    Console.WriteLine("Ingrese el monto del gasto:");
                     string? gasto = Console.ReadLine();
                    if (decimal.TryParse(gasto, out decimal gastoDecimal))
                    {
                         balance -= gastoDecimal;
                        using (StreamWriter sw = File.AppendText(Finanzas))
                        {
                        
                          sw.WriteLine("Balance actual: " + balance);
                        }
                        using (StreamWriter sw = File.AppendText(Historial))
                        {
                            sw.WriteLine("Gasto: " + gastoDecimal + " - " + DateTime.Now);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Monto no válido, por favor intente de nuevo.");
                    }
                    break;
                case "3":
                    using (StreamReader srw = File.OpenText(Finanzas))
                        {
                            string b = File.ReadLines(Finanzas).Last();
                            Console.WriteLine(b);
                                
                        }
                    break;
                case "4":
                    // Ver historial
                    Console.WriteLine("Historial de transacciones:");
                    using (StreamReader srw = File.OpenText(Historial))
                    {
                        string? linea;
                        while ((linea = srw.ReadLine()) != null)
                        {
                            Console.WriteLine(linea);
                        }
                    }
                    break;
                case "5":
                   Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }

            
        }
        
    }

}



