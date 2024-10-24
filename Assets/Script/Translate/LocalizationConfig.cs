using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalizationConfig
{
    private static Dictionary<string, Dictionary<string, string>> languageData = new Dictionary<string, Dictionary<string, string>>()
    {
        // Данные для разных языков
        {
            "RU", new Dictionary<string, string>()
            {
                {"StartText"," \"TradeMaster Challenge\" — это обучающая игра, разработанная для обучения пользователей основам анализа рыночных трендов и принятия решений без использования реальных денег и финансовых манипуляций. Игра предлагает несколько режимов, включая \"Анализ трендов\" и \"Экономическая викторина\", которые позволяют пользователям развивать свои навыки в увлекательной игровой форме."},
                {"NextText","Далее"},
                {"Placeholder","Имя пользователя"},
                {"TextThemeBG","Тема"},
                {"TextLangBg","Язык"},
                {"Settings","Настройки"},
                {"Privacy","Политика конфиденциальности \\nи \\nПравила общения"},
                {"Modes","Режимы"},
                {"Trade Master Challenge","Торговый Мастер: Вызов"},
                {"Trend analysis","Анализ трендов"},
                {"Economic quiz","Экономическая викторина"},
                {"Achievements and awards","Достижения и награды"},
                {"Trov","Трофеи:"},
                {"CurrentTradePoint","Набранные очки"},
                {"currentcoins","Криптомонеты"},
                {"Descending","Убывание"},
                {"LeteralText","Боковой"},
                {"Ascending","Возрастание"},
                {"Task","Ваша задача – определить тренд на графике: восходящий, нисходящий, боковой."},
                {"Clue","Возьмите подсказку за 50."},
                {"AnswereNext", "Продолжить"},
            }
        },
        {
            "ENG", new Dictionary<string, string>()
            {
                {"StartText","\"TradeMaster Challenge\" — is an educational game designed to teach users the basics of market trend analysis and decision making without the use of real money and financial manipulation. The game offers several modes, including \"Trend Analysis\" and \"Economic Quiz\", which allow users to develop their skills in a fun game form."},
                {"NextText","Next"},
                {"Placeholder","Username"},
                {"TextThemeBG","Theme"},
                {"TextLangBg","Language"},
                {"Settings","Settings"},
                {"Privacy","Privacy Policy \nand \nCommunity Guidlines"},
                {"Modes","Modes"},
                {"Trade Master Challenge","TradeMaster\nChallenge"},
                {"Trend analysis","Trend analysis"},
                {"Economic quiz","Economic quiz"},
                {"Achievements and awards","Achievements and awards"},
                {"Trov","Trophies:"},
                {"CurrentTradePoint","Points scored"},
                {"currentcoins","Crypto coins"},
                {"Descending","Descending"},
                {"LeteralText","Lateral"},
                {"Ascending","Ascending"},
                {"Task","Your task is to determine the trend on the chart           - upward, downward, sideways"},
                {"Clue","Take a clue for        50"},
                {"AnswereNext", "Continue"},
                
            }
        }
    };

    public static Dictionary<string, string> GetLocalizationData(string language)
    {
        // Если данные для указанного языка есть, возвращаем их, иначе возвращаем данные для английского языка
        return languageData.ContainsKey(language) ? languageData[language] : languageData["RU"];
    }
}
