   M S B A M L   `   `   `  ����  ВКанализации)    #ВКанализации.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��400��6   $	��600��6)   $	��400��65   $	��350��6E   .��5	      � 
���iii� � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $?��9Добро пожаловать ВКанализацию!��6   $��20(�6:   
�������$��0��5      $��Center=�6!   $��Center=�6<   5      �� 5   
   -   5         ��Name$ Username��5      $�� ��6$   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$= 7Введите свое имя пользователя��5      $��20(�6Q   
�������$��1��5      $��Center=�6   $��Center=�6:   $��20q�6W   ��5      �� *ActualWidth+ �� RelativeSource ��  �� AncestorType" �.��5   d   5      �� 5   
   -   5         ��Name$ CreateNewButton��5      $2��,Я хочу создать новый чат��5      $��20(�64   
�������$��2��5      $��Center=�6   $��Center=�69   $��20q�6V   ��5      �� *ActualWidth+ �� " �5      �� 5    
   -   5        $ ConnectToButton��5       $*��$Я хочу подключиться��5!      $��20(�6/   
�������$��3��5$      $��Center=�6   $��Center=�69   $��20q�6V   ��5"      �� *ActualWidth+ �� " �5%      �� 5&   
   -   5        $ 	IpAddress��5&      $�� ��6%   $ IP адрес чата��5'      $��20(�6A   
�������$��4��5*      $��Center=�6   $��Center=�6:   $��20q�6W   ��5(      �� *ActualWidth+ �� " �.��5*   d   5+      5,      5-      