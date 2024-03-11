using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_.net_
{
    public class QueryMethods
    {
        private readonly DataFilling _dataFilling;
        public QueryMethods(DataFilling datafilling)
        {
            _dataFilling = datafilling;
        }

        public void GetAllAssets()
        {
            var q1 = from x in _dataFilling.assets
                     select x;
            Console.WriteLine("Вивести всю iнформацiю про основнi засоби: ");
            foreach (var x in q1)
                Console.WriteLine($"Id: {x.Id}, Iнвентарний номер: {x.InventoryNumber}, " +
                    $"Назва засобу: {x.Name}, Первiсна вартiсть: {x.InitialCost}," +
                    $" DepartmentId: {x.DepartmentId}, ResponsiblePersonId: {x.ResponsiblePersonId}");
        }

        public void GetResponsiblePetsonsById()
        {
            var q3 = from p in _dataFilling.ResponsibleRersons
                     where p.Id > 3 && p.Id < 8
                     orderby p.Surname descending
                     select p;
            Console.WriteLine($"\nСписок вiдповiдальних персон id, яких бiльше 3, але менше 8 ");
            foreach (var p in q3)
                Console.WriteLine($"Id: {p.Id}, Прiзвище людини: {p.Surname}, Iм'я: {p.Name}");
        }

        public void GetAssetsByProducedDepartment()
        {
            var q4 = from asset in _dataFilling.assets
                     join department in _dataFilling.departments on asset.DepartmentId equals department.Id
                     where department.DepartmentName == "Виробничий пiдроздiл"
                     select new { asset.Name, department.DepartmentName };
            Console.WriteLine($"\n Засоби, якi належать вiддiлу \"Виробничий пiдроздiл\"");
            foreach (var assetDepartment in q4)
                Console.WriteLine($"Засiб: {assetDepartment.Name} - Вiддiл: {assetDepartment.DepartmentName}");
        }

        public void GroupAssetsByDepartments()
        {
            var q5 = from asset in _dataFilling.assets
                     join department in _dataFilling.departments on asset.DepartmentId equals department.Id
                     group asset by department into g
                     select new
                     {
                         Department = g.Key,
                         Assets = g.ToList()
                     };
            Console.WriteLine($"\nГрупування засобiв за вiддiлами");
            foreach (var group in q5)
            {
                Console.WriteLine($"Department: {group.Department.DepartmentName}");
                foreach (var asset in group.Assets)
                {
                    Console.WriteLine($"Asset Name: {asset.Name}," +
                        $" Inventory Number: {asset.InventoryNumber}, Initial Cost: {asset.InitialCost}");
                }
                Console.WriteLine();
            }
        }

        public void GetAssetsByDocuments()
        {
            var q6 = from asset in _dataFilling.assets
                     join assetDocument in _dataFilling.assetDocuments on asset.Id equals assetDocument.AssetId
                     join document in _dataFilling.documents on assetDocument.DocumentId equals document.Id
                     orderby asset.Name
                     select new { asset.Name, document.DocumentName, assetDocument.Date };
            Console.WriteLine("Вивести основнi засоби за документами у порядку зростання");
            foreach (var asset in q6)
            {
                Console.WriteLine($"Засiб: {asset.Name} - {asset.DocumentName} Дата органiзацiї: {asset.Date}");
            }
        }

        public void GetDocumentsByAsset()
        {
            var q7 = _dataFilling.assets
                .Join(_dataFilling.assetDocuments,
                     asset => asset.Id,
                     assetDocument => assetDocument.AssetId,
                     (asset, assetDocument) => new { Asset = asset, AssetDocument = assetDocument })
                .Join(_dataFilling.documents,
                     combined => combined.AssetDocument.DocumentId,
                     document => document.Id,
                     (combined, document) => new { AssetName = combined.Asset.Name, DocumentName = document.DocumentName })
                .Where(combined => combined.AssetName == "Комп'ютер офiсний")
                .Select(combined => new { combined.AssetName, combined.DocumentName });

            Console.WriteLine("\nВивести документи, якi є у основного засоба \"Комп'ютер офiсний\"");
            foreach (var asset in q7)
            {
                Console.WriteLine($"Засiб: {asset.AssetName} - {asset.DocumentName}");
            }
        }

        public void GetDepartmetsStartWithA()
        {
            Console.WriteLine("\nВивести вiддiли, якi починаються на букву \"А\" i вiдсортувати їх у порядку зростання");
            var q8 = _dataFilling.departments
                .Where(department => department.DepartmentName.StartsWith("А"))
                .OrderBy(department => department.DepartmentName)
                .Select(department => new { department.Id, department.DepartmentName });
            foreach (var department in q8)
            {
                Console.WriteLine($"Вiддiл: {department.Id} - {department.DepartmentName}");
            }
        }

        public void GetAssetsWithMaxAndMinCost()
        {
            Console.WriteLine("\nОтримати основнi засоби з найвищою та найнижчою цiною");
            decimal highestCost = _dataFilling.assets.Max(asset => asset.InitialCost);
            decimal lowestCost = _dataFilling.assets.Min(asset => asset.InitialCost);

            var assetHighestCost = _dataFilling.assets
                .Where(asset => asset.InitialCost == highestCost)
                .Select(asset => new { asset.InventoryNumber, asset.Name, asset.InitialCost });

            var assetLowestCost = _dataFilling.assets
                .Where(asset => asset.InitialCost == lowestCost)
                .Select(asset => new { asset.InventoryNumber, asset.Name, asset.InitialCost });

            var q9 = assetHighestCost.Concat(assetLowestCost);
            foreach (var asset in q9)
            {
                Console.WriteLine($"Засiб: {asset.InventoryNumber} {asset.Name} - {asset.InitialCost} грн");
            }
        }

        public void GetAssetsByResponsiblePerson()
        {
            var q10 = _dataFilling.ResponsibleRersons
                .Join(_dataFilling.assets,
                      responsiblePerson => responsiblePerson.Id,
                      asset => asset.ResponsiblePersonId,
                      (responsiblePerson, asset) =>
                      new {
                          Asset = asset,
                          ResponsiblePerson = responsiblePerson
                      })
                .Where(combined => combined.ResponsiblePerson.Name == "Андрiй"
                       && combined.ResponsiblePerson.Surname == "Бондаренко")
                .Select(combined => new {
                    AssetName = combined.Asset.Name,
                    ResponsiblePersonSurname = combined.ResponsiblePerson.Surname,
                    ResponsiblePersonName = combined.ResponsiblePerson.Name
                });
            Console.WriteLine("\nВивести основнi засоби, якi належать Андрiю Бондаренку");
            foreach (var asset in q10)
            {
                Console.WriteLine($"Вiдповiдальна персона: {asset.ResponsiblePersonSurname} " +
                    $"{asset.ResponsiblePersonName} - {asset.AssetName}");
            }
        }

        public void GroupDepartmentsByAssetCount ()
        {
            var q11 = from department in _dataFilling.departments
                      join asset in _dataFilling.assets on department.Id equals asset.DepartmentId
                      group asset by department.DepartmentName into grouped
                      select new { Department = grouped.Key, Count = grouped.Count() };
            Console.WriteLine("\nЗгрупувати вiддiли, за кiлькiстю основних засобiв, якi їм належать");
            foreach (var group in q11)
            {
                Console.WriteLine($"Вiддiл: {group.Department} - {group.Count}");
            }
        }

        public void GetDocumentsByAssetAndDate()
        {
            var q12 = from asset in _dataFilling.assets
                      join assetDocument in _dataFilling.assetDocuments on asset.Id equals assetDocument.AssetId
                      join document in _dataFilling.documents on assetDocument.DocumentId equals document.Id
                      where asset.Name == "Принтер лазерний" && assetDocument.Date >= new DateTime(2023, 8, 1)
                      && assetDocument.Date <= new DateTime(2024, 6, 11)
                      select new
                      {
                          AssetName = asset.Name,
                          DocumentName = document.DocumentName,
                          DocumentDate = assetDocument.Date
                      };
            Console.WriteLine("\nОтримати документи основного засобу Принтер лазерний " +
                "i\n якi були зробленi в перiод вiд 1 серпня 2023 рокi по 11 червня 2024 року");
            foreach (var document in q12)
            {
                Console.WriteLine($"{document.AssetName}: {document.DocumentName} {document.DocumentDate}");
            }
        }

        public void GetDepartmentByAssetAndInventaryNum()
        {
            var q13 = _dataFilling.departments
                .Join(_dataFilling.assets, department => department.Id,
                    asset => asset.DepartmentId,
                    (department, asset) =>
                        new { Asset = asset, Department = department })
                .Where(combined => combined.Asset.InventoryNumber == 1301
                || combined.Asset.InventoryNumber == 1204)
                .Select(combined => new {
                    AssetNum = combined.Asset.InventoryNumber,
                    AssetName = combined.Asset.Name,
                    Department = combined.Department.DepartmentName
                });
            Console.WriteLine("\n Отримати пiдроздiли, у якому обслуговуються " +
                "основнi засоби з iнвентарним номером 1301 або 1204");
            foreach (var department in q13)
            {
                Console.WriteLine($" {department.AssetNum} {department.AssetName} - {department.Department}");
            }
        }

        public void GetResponsiblePersonByAssetID()
        {
            var q14 = from responsiblePerson in _dataFilling.ResponsibleRersons
                      join asset in _dataFilling.assets on responsiblePerson.Id equals asset.ResponsiblePersonId
                      where asset.Id >= 5 && asset.Id <= 9
                      select new { asset.Id, asset.Name, responsiblePerson.Surname, responsiblePerson.Phone };
            Console.WriteLine("\n Отримати прiзвище та номер телефону вiдповiдальної особи " +
                "за основний засiб у якого id бiльше 5, але менше 9");
            foreach (var responsiblePerson in q14)
            {
                Console.WriteLine($" {responsiblePerson.Id} {responsiblePerson.Name} - вiдповiдальна особа {responsiblePerson.Surname} {responsiblePerson.Phone}");
            }
        }

        public void GetTotalCostByAsset()
        {
            var q15 = _dataFilling.assets.Sum(asset => asset.InitialCost);
            Console.WriteLine("\nОтримати загальну цiну всiх основних засобiв" + " - " + q15 + "грн");
        }

        public void GetAssetsSortByCost()
        {
            var q16 = from asset in _dataFilling.assets
                      orderby asset.InitialCost
                      select asset;
            Console.WriteLine("\nВiдсортувати основнi засоби за цiною");
            foreach (var asset in q16)
            {
                Console.WriteLine($"{asset.Id} {asset.InventoryNumber} {asset.Name} {asset.InitialCost}");
            }
        }
        public void GetAssetTotalCount()
        {
            var q17 = _dataFilling.assets.Count(asset => asset?.Id != null);
            Console.WriteLine($"\nОтримати загальну кiлькiсть основних засобiв {q17}");
        }

        public void GetResponsiblePersonStartsWithP()
        {
            var q18 = from responsiblePerson in _dataFilling.ResponsibleRersons
                      where responsiblePerson.Surname.StartsWith("П")
                      select responsiblePerson;
            Console.WriteLine($"\nОтримати вiдповiдальних осiб прiзвища, яких починаються на П");
            foreach (var responsibleRerson in q18)
            {
                Console.WriteLine($"{responsibleRerson.Surname} {responsibleRerson.Name} {responsibleRerson.Phone}");
            }
        }

        public void GetAssetsEndsWith()
        {
            var q19 = _dataFilling.assets.Where(asset => asset.Name.EndsWith("ний"));
            Console.WriteLine($"\nОтримати основнi засоби, якi закiнчуються на -ний");
            foreach (var asset in q19)
            {
                Console.WriteLine($"{asset.InventoryNumber} {asset.Name} {asset.InitialCost} грн");
            }
        }

        public void GroupResponsiblePersonByAsset()
        {
            var q20 = _dataFilling.ResponsibleRersons.Join(_dataFilling.assets,
               responsiblePerson => responsiblePerson.Id,
               asset => asset.ResponsiblePersonId,
               (responsiblePerson, asset) => new { Asset = asset, ResponsibleRerson = responsiblePerson })
               .GroupBy(combined => combined.ResponsibleRerson.Surname, combined => combined.Asset.Id,
               (key, g) => new { ResponsiblePerson = key, Count = g.Count() });
            Console.WriteLine("\nЗгрупувати вiдповiдальнi персони, за кiлькiстю основних засобiв, якi їм належать");
            foreach (var group in q20)
            {
                Console.WriteLine($"Вiдповiдальна персона: {group.ResponsiblePerson} - {group.Count}");
            }
        }

        public void GetAssetContainsThreeWords()
        {
            var q21 = from asset in _dataFilling.assets
                      where asset.Name.Split(' ').Length == 3
                      select asset;
            Console.WriteLine($"\nОтримати основнi засоби, в яких назва складається з 3-х слiв");
            foreach (var asset in q21)
            {
                Console.WriteLine($"{asset.InventoryNumber} {asset.Name} {asset.InitialCost} грн");
            }
        }
    }
}
