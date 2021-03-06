﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calc10
{
    /// <summary>
    /// Logique d'interaction pour Date.xaml
    /// </summary>
    public partial class Date : UserControl
    {
        private readonly int[] comboBoxList = Enumerable.Range(0, 999).ToArray();

        public Date()
        {
            InitializeComponent();
        }
        //LOAD
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (int num in comboBoxList)
            {
                yearSelectComboBox.Items.Add(num);
                monthSelectComboBox.Items.Add(num);
                daySelectComboBox.Items.Add(num);
            }
            starterDate.SelectedDate = DateTime.Now;
            daySelectComboBox.SelectedIndex = 0;
            monthSelectComboBox.SelectedIndex = 0;
            yearSelectComboBox.SelectedIndex = 0;
        }

        //DIFFERENCE SECTION
        private void fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int day = 0, week = 0, month = 0, year = 0;


            if (fromDate.SelectedDate != null && toDate.SelectedDate != null)
            {
                DateTime fromdate = (DateTime)fromDate.SelectedDate;
                DateTime todate = (DateTime)toDate.SelectedDate;

                int ts = Convert.ToInt32(Math.Abs((todate - fromdate).TotalDays));

                if (ts < 7)
                {
                    day = ts;
                }
                else if (ts >= 7 && ts < 30)
                {
                    week = ts / 7;
                    day = ts % 7;
                }
                else if (ts >= 30 && ts < 365)
                {
                    month = ts / 30;
                    week = (ts % 30) / 7;
                    day = (ts % 30) % 7;
                }
                else if (ts >= 365)
                {
                    year = ts / 365;
                    month = (ts % 365) / 30;
                    week = (ts % 365) % 30 / 7;
                    day = (ts % 365) % 30 % 7;
                }

                if (week == 0 && month == 0 && year == 0)
                {
                    diffDate.Text = ts + " Day(s)";
                }
                else
                {
                    diffDate.Text = removeUselessNumbers(day, week, month, year) + " Or " + ts + " Total Days";
                }
            }
        }

        private void toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int day = 0, week = 0, month = 0, year = 0;


            if (fromDate.SelectedDate != null && toDate.SelectedDate != null)
            {
                DateTime fromdate = (DateTime)fromDate.SelectedDate;
                DateTime todate = (DateTime)toDate.SelectedDate;

                int ts = Convert.ToInt32(Math.Abs((todate - fromdate).TotalDays));

                if (ts < 7)
                {
                    day = ts;
                }
                else if (ts >= 7 && ts < 30)
                {
                    week = ts / 7;
                    day = ts % 7;
                }
                else if (ts >= 30 && ts < 365)
                {
                    month = ts / 30;
                    week = (ts % 30) / 7;
                    day = (ts % 30) % 7;
                }
                else if (ts >= 365)
                {
                    year = ts / 365;
                    month = (ts % 365) / 30;
                    week = (ts % 365) % 30 / 7;
                    day = (ts % 365) % 30 % 7;
                }

                if (week == 0 && month == 0 && year == 0)
                {
                    diffDate.Text = ts + " Day(s)";
                }
                else
                {
                    diffDate.Text = removeUselessNumbers(day, week, month, year) + " Or " + ts + " Total Days";
                }
            }
        }
        private string removeUselessNumbers(int day, int week, int month, int year)
        {
            string result = "", dayStr = "", weekStr = "", monthStr = "", yearStr = "";

            if (day == 1)
            {
                dayStr = day + " Day";
            }
            else if (day != 0)
            {
                dayStr = day + " Days";
            }

            if (week == 1)
            {
                weekStr = week + " Week";
            }
            else if (week != 0)
            {
                weekStr = week + " Weeks";
            }

            if (month == 1)
            {
                monthStr = month + " Month";
            }
            else if (month != 0)
            {
                monthStr = month + " Months";
            }

            if (year == 1)
            {
                yearStr = year + " Year";
            }
            else if (year != 0)
            {
                yearStr = year + " Years";
            }

            if (day != 0)
            {
                result = dayStr;
            }

            if (week != 0)
            {
                result = weekStr + "; " + result;
            }

            if (month != 0)
            {
                result = monthStr + "; " + result;
            }

            if (year != 0)
            {
                result = yearStr + "; " + result;
            }

            return result;
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(50);
            difference.Visibility = Visibility.Hidden;
            addSub.Visibility = Visibility.Hidden;

            if (Selection.SelectedIndex == 0)
            {
                difference.Visibility = Visibility.Visible;
            }
            else
            {
                addSub.Visibility = Visibility.Visible;
            }
        }
        //DIFFERENCE SECTION END

        private void starterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime d1 = DateTime.Today;
            starterDate.SelectedDate = DateTime.Today;
            if (yearSelectComboBox.SelectedItem != null || monthSelectComboBox.SelectedItem != null || daySelectComboBox.SelectedItem != null || starterDate.SelectedDate != null)
            {
                d1 = (DateTime)starterDate.SelectedDate;
                if (addButton.IsChecked == true)
                {
                    d1 = d1.AddYears(yearSelectComboBox.SelectedIndex);
                    d1 = d1.AddMonths(monthSelectComboBox.SelectedIndex);
                    d1 = d1.AddDays(daySelectComboBox.SelectedIndex);
                }
                else if (subButton.IsChecked == true)
                {
                    d1 = d1.AddYears((yearSelectComboBox.SelectedIndex) * -1);
                    d1 = d1.AddMonths((monthSelectComboBox.SelectedIndex) * -1);
                    d1 = d1.AddDays((daySelectComboBox.SelectedIndex) * -1);
                }
                string d2 = d1.ToString();
                d2 = d2.Substring(0, 10);
                subAddResultTextBox.Text = d2;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime d1 = DateTime.Today;
            starterDate.SelectedDate = DateTime.Today;
            if (yearSelectComboBox.SelectedItem != null || monthSelectComboBox.SelectedItem != null || daySelectComboBox.SelectedItem != null || starterDate.SelectedDate != null)
            {
                d1 = (DateTime)starterDate.SelectedDate;
                if (addButton.IsChecked == true)
                {
                    d1 = d1.AddYears(yearSelectComboBox.SelectedIndex);
                    d1 = d1.AddMonths(monthSelectComboBox.SelectedIndex);
                    d1 = d1.AddDays(daySelectComboBox.SelectedIndex);
                }
                else if (subButton.IsChecked == true)
                {
                    d1 = d1.AddYears((yearSelectComboBox.SelectedIndex) * -1);
                    d1 = d1.AddMonths((monthSelectComboBox.SelectedIndex) * -1);
                    d1 = d1.AddDays((daySelectComboBox.SelectedIndex) * -1);
                }
                string d2 = d1.ToString();
                d2 = d2.Substring(0, 10);
                subAddResultTextBox.Text = d2;
            }
        }
    }
}
