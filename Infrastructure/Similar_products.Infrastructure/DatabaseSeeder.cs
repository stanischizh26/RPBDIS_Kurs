using Bogus;
using Similar_products.Domain.Entities;
using Similar_products.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Автоматически применяем миграции
        _context.Database.EnsureCreated();

        Random rand = new(0);
        // Если таблица пустая то ее заполнить
        if (!_context.Products.Any())
        {
            InitializeProducts(productsNumber: 30, rand);
        }
        if (!_context.Enterprises.Any())
        {
            InitializeEnterprises(enterprisesNumber: 30, rand);
        }
        if (!_context.ProductionPlans.Any())
        {
            InitializeProductionPlans(productionPlansNumber: 30, rand);
        }
    }
    private void InitializeProducts(int productsNumber, Random rand)
    {
        string[] productName = ["Ноутбук Lenovo ThinkPad", "Смартфон Samsung Galaxy", "Холодильник LG",
                         "Кабель HDMI", "Пакет офисной бумаги", "Картридж для принтера HP",
                         "Молоко", "Мука", "Вода питьевая", "Бензин"];

        string[] productCharacteristics = ["Производительность для работы и учебы", "Большой экран и мощная камера",
                                    "Энергоэффективность и вместительность", "Подключение устройств",
                                    "Для офисных и учебных задач", "Расходный материал для печати",
                                    "Свежесть и качество", "Пекарное использование", "Чистота и минерализация",
                                    "Топливо для транспортных средств"];

        string[] productUnit = ["Штука", "Метр", "Пачка", "Штука",
                         "Литр", "Килограмм", "Литр", "Литр"];

        int countProductName = productName.GetLength(0);
        int countProductCharacteristics = productCharacteristics.GetLength(0);
        int countProductUnit = productUnit.GetLength(0);

        for (int i = 0; i < productsNumber; i++)
        {
            _context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = productName[rand.Next(countProductName)],
                Characteristics = productCharacteristics[rand.Next(countProductCharacteristics)],
                Unit = productUnit[rand.Next(countProductUnit)],
                Photo = "No Photo",
            });
        }
        _context.SaveChanges();
    }

    private void InitializeEnterprises(int enterprisesNumber, Random rand)
    {
        string[] enterpriseName = {"АльфаСтрой", "ТехноСервис", "ИнноваПром", "ГлобалТрейд", "ЭкоФуд",
                                "МедФарм", "АгроЛэнд", "БизнесПроект", "ФинансыПлюс", "ЛогистикСеть",
                                "РемонтСервис", "ТехАвто", "СофтЭксперт", "ГоризонтТур", "ДизайнСтудия",
                                "КнигаМир", "МедиаЦентр", "КосметикПро", "СтройГарант", "Вкусноеда"
};

        string[] enterpriseDirectorName = {"Иванов Иван Иванович", "Петрова Мария Сергеевна", "Сидоров Петр Алексеевич", "Антонова Анна Викторовна",
                                "Смирнов Сергей Павлович", "Александрова Екатерина Николаевна", "Дмитриев Дмитрий Андреевич", "Ильина Ольга Романовна",
                                "Кузнецов Николай Васильевич", "Титова Светлана Ивановна", "Николаева Анастасия Владимировна", "Максимов Максим Степанович",
                                "Викторова Виктория Юрьевна", "Андреев Андрей Григорьевич", "Еленина Елена Арсеньевна", "Романов Роман Константинович",
                                "Ксенина Ксения Владиславовна", "Владимиров Владимир Петрович", "Дарьева Дарья Александровна", "Игорев Игорь Михайлович"
};
        string[] enterpriseActivityType = {"Строительство", "Торговля", "Производство", "Образование", "Медицинские услуги",
                                "Туризм", "Транспорт и логистика", "ИТ-услуги", "Консалтинг", "Финансовая деятельность",
                                "Ремонт и обслуживание техники", "Агропромышленность", "Пищевая промышленность", "Энергетика",
                                "Творческие услуги", "Рекламная деятельность", "Юридические услуги", "Недвижимость",
                                "Банковская деятельность", "Производство одежды"
};

        string[] enterpriseOwnershipForm = {"ООО", "ИП", "АО", "ПАО", "Государственное предприятие",
                                "Муниципальное предприятие", "Хозяйственное товарищество", "Некоммерческая организация"
};


        int countEnterpriseName = enterpriseName.GetLength(0);
        int countEnterpriseActivityType = enterpriseActivityType.GetLength(0);
        int countEnterpriseDirectorName = enterpriseDirectorName.GetLength(0);
        int countEnterpriseOwnershipForm = enterpriseOwnershipForm.GetLength(0);


        for (int i = 0; i < enterprisesNumber; i++)
        {
            _context.Enterprises.Add(new Enterprise
            {
                Id = Guid.NewGuid(),
                Name = enterpriseName[rand.Next(countEnterpriseName)],
                DirectorName = enterpriseDirectorName[rand.Next(countEnterpriseDirectorName)],
                ActivityType = enterpriseActivityType[rand.Next(countEnterpriseActivityType)],
                OwnershipForm = enterpriseOwnershipForm[rand.Next(countEnterpriseOwnershipForm)],
            });
        }
        _context.SaveChanges();
    }

    private void InitializeProductionPlans(int productionPlansNumber, Random rand)
    {
        int[] productionPlanPlannedVolume = { 1200, 1700, 1400, 1500, 1800, 1600 };

        int[] productionPlanActualVolume = { 600, 1000, 800, 1653, 1986, 1822 };

        byte[] productionPlanQuarter = { 1, 2, 3, 4 };

        int[] productionPlanYear = { 1999, 2021, 2020, 2022, 2024, 2019, 2005, 2007, 2018 };

        int countProductionPlanPlannedVolume = productionPlanPlannedVolume.GetLength(0);
        int countProductionPlanActualVolume = productionPlanActualVolume.GetLength(0);
        int countproductionPlanQuarter = productionPlanQuarter.GetLength(0);
        int countProductionPlanYear = productionPlanYear.GetLength(0);


        for (int i = 0; i < productionPlansNumber; i++)
        {
            var enterprise = _context.Enterprises.OrderBy(e => Guid.NewGuid()).FirstOrDefault();
            var product = _context.Products.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            if (enterprise == null && product == null) continue;
            _context.ProductionPlans.Add(new ProductionPlan
            {
                Id = Guid.NewGuid(),
                PlannedVolume = productionPlanPlannedVolume[rand.Next(countProductionPlanPlannedVolume)],
                ActualVolume = productionPlanActualVolume[rand.Next(countProductionPlanActualVolume)],
                Year = productionPlanYear[rand.Next(countProductionPlanYear)],
                ProductId = product.Id,
                EnterpriseId = enterprise.Id,
                Quarter = productionPlanQuarter[rand.Next(countproductionPlanQuarter)]
            });
        }
        _context.SaveChanges();
    }

    private void InitializeProductTypes(int productTypesNumber, Random rand)
    {
        string[] productTypeName = { "Электроника", "Бытовая техника", "Канцелярские товары", "Продукты питания", "Топливо" };

        int countproductTypeName = productTypeName.GetLength(0);

        for (int i = 0; i < productTypesNumber; i++)
        {
            _context.ProductTypes.Add(new ProductType
            {
                Id = Guid.NewGuid(),
                Name = productTypeName[rand.Next(countproductTypeName)],
            });
        }
        _context.SaveChanges();
    }

    private void InitializeSalesPlans(int salesPlansNumber, Random rand)
    {
        int[] salesPlanPlannedSales = { 1200, 1700, 1400, 1500, 1800, 1600 };
        int[] salesPlanActualSales = { 600, 1000, 800, 1653, 1986, 1822 };
        byte[] salesPlanQuarter = { 1, 2, 3, 4 };
        int[] salesPlanYear = { 1999, 2021, 2020, 2022, 2024, 2019, 2005, 2007, 2018 };


        int countsalesPlanPlannedSales = salesPlanPlannedSales.GetLength(0);
        int countsalesPlanActualSales = salesPlanActualSales.GetLength(0);
        int countsalesPlanQuarter = salesPlanQuarter.GetLength(0);
        int countsalesPlanYear = salesPlanYear.GetLength(0);

        for (int i = 0; i < salesPlansNumber; i++)
        {
            _context.SalesPlans.Add(new SalesPlan
            {
                Id = Guid.NewGuid(),
                PlannedSales = salesPlanPlannedSales[rand.Next(countsalesPlanPlannedSales)],
                ActualSales = salesPlanActualSales[rand.Next(countsalesPlanActualSales)],
                Quarter = salesPlanQuarter[rand.Next(countsalesPlanQuarter)],
                Year = salesPlanYear[rand.Next(countsalesPlanYear)],
            });
        }
        _context.SaveChanges();
        _context.SaveChanges();
    }
}
