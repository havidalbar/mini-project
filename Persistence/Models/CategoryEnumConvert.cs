using System;
namespace Persistence.Models
{
	public static class CategoryEnumConvert
	{
        public static string ToCustomString(CategoryEnum category)
        {
            switch (category)
            {
                case CategoryEnum.Task:
                    return "Task";
                case CategoryEnum.DailyActivity:
                    return "DailyActivity";
                default:
                    return "Unknown Category";
            }
        }

        public static object ToCustomEnum(string category)
        {
            switch (category)
            {
                case "Task":
                    return CategoryEnum.Task;
                case "DailyActivity":
                    return CategoryEnum.DailyActivity;
                default:
                    return null;
            }
        }
    }
}

