using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    abstract class State
    {
        public abstract void Handle(ATM context);
    }

    class Waiting : State
    {
        public override void Handle(ATM ATM)
        {
            ATM.State = new Authentication();
        }
    }

    class Authentication : State
    {
        public override void Handle(ATM ATM)
        {
            ATM.State = new Operaions();
        }
    }

    class Operaions : State
    {
        public override void Handle(ATM ATM)
        {
            if(ATM.totalAmount == 0)
                ATM.State = new IsLocked();
            else
                ATM.State = new Waiting();
        }
    }

    class IsLocked : State
    {
        public override void Handle(ATM ATM)
        {
            ATM.State = new Waiting();
        }
    }

    class ATM
    {
        public int ID, totalAmount;
        public double connectionPorbability;

        public State State { get; set; }

        public ATM(State state, int ID, int totalAmount, double connectionPorbability)
        {
            this.State = state;
            this.ID = ID;
            this.totalAmount = totalAmount;
            this.connectionPorbability = connectionPorbability;
            Waiting();
        }

        public void Request()
        {
            this.State.Handle(this);
        }


        public void Waiting()
        {
            Console.WriteLine(State);
            Console.WriteLine("Введите что-ниубдь для начала работы.");
            Console.ReadKey();
            if (this.totalAmount != 0)
            {
                this.State.Handle(this);
                EnterPin();
            }
            else
            {
                Console.WriteLine("Банкомат заблокирован!\n" +
                    "Хотите внести деньги? 1 - да/ 0 - нет");

                int choise = Int32.Parse(Console.ReadLine());
                if (choise == 1)
                {
                    Console.WriteLine("Какую сумму вы хотите внести?");
                    int amount = Int32.Parse(Console.ReadLine());
                    this.totalAmount = this.totalAmount + amount;
                    this.State.Handle(this);
                    Console.WriteLine("Деньги внесены!");
                }
                else
                {
                    Console.WriteLine("До свидания!");
                    Waiting();
                }

            }
        }

        public void EnterPin()
        {
            Console.WriteLine(State);
            Console.WriteLine("Добро пожаловать в банкомат! Введите, пожалуйста, ваш PIN-код.");
            int pin = Int16.Parse(Console.ReadLine());
            int choise;
            do
            {
                choise = 0;
                if (pin == 1234)     //либо другой пин код пользователя
                {
                    Console.WriteLine("PIN-код верен!");
                    this.State.Handle(this);
                    MainMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("PIN-код не верен! Введите '1', если хотите попробовать еще раз.");
                    choise = Int16.Parse(Console.ReadLine());
                }
              
            } while (choise == 1);

            if(choise != 1)
            {
                this.State.Handle(this);
                FinishWork();
            }
        }


        public void MainMenu()
        {
            Console.WriteLine(State);
            Console.WriteLine("Для выбора пункта меню напишите в коносле нужную цифру.\n" +
                "\tСнять деньги - 1\n" +
                "\tПополнить счет - 2\n" +
                "\tЗавершить работу - 3");
            int choise = Int16.Parse(Console.ReadLine());
            if (choise == 1)
                Withdraw();
            else if (choise == 2)
                AddMoney();
            else if (choise == 3)
                FinishWork();
            else
            {
                Console.WriteLine("Вы ввели неверное значение! Экстренное завершение работы!");
                FinishWork();
            }
        }


        public void Withdraw()
        {
            Console.WriteLine("Какую сумму вы хотите снять?");
            int amount = Int32.Parse(Console.ReadLine());

            if (this.totalAmount >= amount)
            {
                this.totalAmount = this.totalAmount - amount;
                Console.WriteLine("Было снято "+amount+" рублей");
                FinishWork();
            }
            else
            {
                Console.WriteLine("В банкомате нет такой суммы!\n Хотите ли вы снять " + this.totalAmount +" рублей?\n" +
                    "1 - да, хочу\n" +
                    "0 - нет, не хочу");
                int choise = Int32.Parse(Console.ReadLine());
                if (choise == 1)
                {
                    this.totalAmount = 0;
                    FinishWork();
                }
                else if (choise == 0)
                {
                    FinishWork();
                }
                else 
                {
                    Console.WriteLine("Вы ввели неверное значение! Экстренное завершение работы!");
                    FinishWork();
                }
            }
        }

        public void AddMoney()
        {
            Console.WriteLine("Какую сумму вы хотите внести?");
            int amount = Int32.Parse(Console.ReadLine());

            this.totalAmount = this.totalAmount + amount;
            Console.WriteLine("Деньги внесены!");
            FinishWork();
        }

        public void FinishWork()
        {
            this.State.Handle(this);
            Console.WriteLine("Всего доброго!");
            Waiting();
        }
    }
}
