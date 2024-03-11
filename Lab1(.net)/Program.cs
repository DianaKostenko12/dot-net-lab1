using Lab1_.net_.Models;
using System;

namespace Lab1_.net_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer = "";
            DataFilling dataFilling = new DataFilling();
            QueryMethods queryMethods = new QueryMethods(dataFilling);
            do
            {
                Console.Clear();
                Console.WriteLine("Виберiть запит:" +
                    "\n1-Вивести всю iнформацiю про основнi засоби" +
                    "\n2-Отримати список вiдповiдальних персон id, яких бiльше 3, але менше 8" +
                    "\n3-Отримати засоби, якi належать вiддiлу \"Виробничий пiдроздiл\"" +
                    "\n4-Групувати засоби за вiддiлами" +
                    "\n5-Вивести основнi засоби за документами у порядку зростання" +
                    "\n6-Вивести документи, якi є у основного засоба \"Комп'ютер офiсний\"" +
                    "\n7-Вивести вiддiли, якi починаються на букву \"А\" i вiдсортувати їх у порядку зростання" +
                    "\n8-Отримати основнi засоби з найвищою та найнижчою цiною" +
                    "\n9-Вивести основнi засоби, якi належать Андрiю Бондаренку" +
                    "\n10-Згрупувати вiддiли, за кiлькiстю основних засобiв, якi їм належать" +
                    "\n11-Отримати документи основного засобу Принтер лазерний  \r\n якi були зробленi в перiод вiд 1 серпня 2023 рокi по 11 червня 2024 року" +
                    "\n12-Отримати пiдроздiли, у якому обслуговуються  \r\nосновнi засоби з iнвентарним номером 1301 або 1204" +
                    "\n13-Отримати прiзвище та номер телефону вiдповiдальної особи  \r\nза основний засiб у якого id бiльше 5, але менше 9" +
                    "\n14-Отримати загальну цiну всiх основних засобiв" +
                    "\n15-Вiдсортувати основнi засоби за цiною" +
                    "\n16-Отримати загальну кiлькiсть основних засобiв" +
                    "\n17-Отримати вiдповiдальних осiб прiзвища, яких починаються на П" +
                    "\n18-Отримати основнi засоби, якi закiнчуються на -ний" +
                    "\n19-Згрупувати вiдповiдальнi персони, за кiлькiстю основних засобiв, якi їм належать" +
                    "\n20-Отримати основнi засоби, в яких назва складається з 3-х слiв"
                    );

                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        queryMethods.GetAllAssets();
                        break;
                    case 2:
                        Console.Clear();
                        queryMethods.GetResponsiblePetsonsById();
                        break;
                    case 3:
                        Console.Clear();
                        queryMethods.GetAssetsByProducedDepartment();
                        break;
                    case 4:
                        Console.Clear();
                        queryMethods.GroupAssetsByDepartments();
                        break;
                    case 5:
                        Console.Clear();
                        queryMethods.GetAssetsByDocuments();
                        break;
                    case 6:
                        Console.Clear();
                        queryMethods.GetDocumentsByAsset();
                        break;
                    case 7:
                        Console.Clear();
                        queryMethods.GetDepartmetsStartWithA();
                        break;
                    case 8:
                        Console.Clear();
                        queryMethods.GetAssetsWithMaxAndMinCost();
                        break;
                    case 9:
                        Console.Clear();
                        queryMethods.GetAssetsByResponsiblePerson();
                        break;
                    case 10:
                        Console.Clear();
                        queryMethods.GroupDepartmentsByAssetCount();
                        break;
                    case 11:
                        Console.Clear();
                        queryMethods.GetDocumentsByAssetAndDate();
                        break;
                    case 12:
                        Console.Clear();
                        queryMethods.GetDepartmentByAssetAndInventaryNum();
                        break;
                    case 13:
                        Console.Clear();
                        queryMethods.GetResponsiblePersonByAssetID();
                        break;
                    case 14:
                        Console.Clear();
                        queryMethods.GetTotalCostByAsset();
                        break;
                    case 15:
                        Console.Clear();
                        queryMethods.GetAssetsSortByCost();
                        break;
                    case 16:
                        Console.Clear();
                        queryMethods.GetAssetTotalCount();
                        break;
                    case 17:
                        Console.Clear();
                        queryMethods.GetResponsiblePersonStartsWithP();
                        break;
                    case 18:
                        Console.Clear();
                        queryMethods.GetAssetsEndsWith();
                        break;
                    case 19:
                        Console.Clear();
                        queryMethods.GroupResponsiblePersonByAsset();
                        break;
                    case 20:
                        Console.Clear();
                        queryMethods.GetAssetContainsThreeWords();
                        break;
                }
                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();
            }
            while (answer == "+");
        }
    }
}