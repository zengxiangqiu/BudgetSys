   M S B A M L   `   `   `  ����D  @BudgetSys, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BudgetSys	BudgetSys  5   	   9!clr-namespace:BudgetSys.ConverterBudgetSys.Converter  5   	       BudgetSys.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:BudgetSys     5   	   7	converter!clr-namespace:BudgetSys.Converter     5   	     � Title$+  %Lenovo ODM Cost Model analysis system��5	   	   $	��550��67   $��1436.206��6D    � Icon$ Images/Logo.png��6U   .��5
      �  � 	Resources 5   
   (   "BudgetSys.Converter.BatchConverter   batchConverter �� BasedOn �� 
TargetType  CenterTitle �� Property �� Value��%  &	        &	      7��  5      6'   �� 5      8   " ��6B   �5      �� 5       � ��$ Center=�6:   6K   �� 5       � ��$ Center=�6<   6M   �� 5       �@��TextAlignment$ Justify=�66   6H   �� 5       � ��$	 4,4q�6/   6=   5      5       � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $��5* �6   6,    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   ]� $��0��6   $��2��6!   .+�5       o� $	��Top=�6   $
��15px(�6(   .@�5!      m� $��主档维护��6   $	��5,5q�6)   .@�5"      m� -   5        $��Metal-Meterials��5"      68   m� 5#      -   5        $��Metal-Tonnages��5#      68   m� 5$      -   5        $��Plastic-Meterials��5$      6;   m� 5%      -   5        $��Plastic-Tonnages��5%      6:   �� 5&      m� 5'      -   5        $��Plastic-SalivaId��5'      6:   �� 5(      m� 5)      -   5        $��ExchangeRate��5)      66   5*      5+      5,      �� 5-   
   $��1,4,1,4q�6   	 �� Orientation$	 
Horizontal=�6&   $��2��6?   $��1��6S   .+�5.      �� #��[ 6   材料类别6C   �� 5/      -   5        $��materialType��5/      $	��4,4q�6,   $
��70px��69   ��6F   �� 
 �� Path$
 MaterialTypes�50      �� 51      $��	批次号��6   #��[ 6#   �� 52      $
��80px��6   $	��4,4q�6#   ��60   �� $
 
BatchInput� �� Mode$ TwoWay=�6`   �� 53      -   5        $��btnNew��53   S   $
��60px��6   $
��20px��6#   $	��4,4q�61   .��6a   新增6e   �� 54      -	   5        $
��60px��54      $
��20px��6#   $	��4,4q�61   .��6T   保存6X   �� 55      -
   5        $
��60px��55      $
��20px��6#   $	��4,4q�61   .��6V   导出6Z   56      �� 57   
   $��批次��6   $��2��6   $��0��6,   $��Stretch=�6<   $��Stretch=�6Z   .��58      ]� .+�59      �� $��Images/Logo.png��6   $
��Fill=�62   $	��100��6B   $	��Top=�6O   6f   �� 5;      -   5        $��0,2,0,5q�5;      $	��Top=�6+   6d   �� 5<      -   5        $��lvBatch��5>      $��Stretch=�5?      $��Stretch=�5@       �� DisplayMemberPath$ batchNo��5C       �� SelectedValuePath$ batchNo��5D       � DataContext 5A      �� ��5B      �� $
 Batches�$ TwoWay=� � ContextMenu 5H      �� 5I      .@�5J      m� -   5        $��btnBatchDelete��5J   3   $��删除��6'   6h   5K      5M      5N      5O      ]� 5P   
   $��2��6   $��1��6!   ��. .+�5Q      �� $��Stretch=�6�   $��Stretch=�6�   ��6   �� $
 CurrentBatch� �� 	Converter# [   �� FallbackValue$ 表格录入��$ TwoWay=�.��5R      &   System.Windows.Controls.DataGrid  -   5        $��dgDetail��5S      $��Stretch=�5T      $��Stretch=�63      AutoGenerateColumns . ��5V      �� $
 Details� �� UpdateSourceTrigger$ 	LostFocus=� 5]      �� 5^      .@�5_      m� -   5        $��	btnDelete��5_   3   $��删除��6'   6^   5`      5b      5c      5d      5f      5g      