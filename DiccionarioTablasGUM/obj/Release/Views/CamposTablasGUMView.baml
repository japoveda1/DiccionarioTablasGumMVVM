   M S B A M L   `   `   `  ���� O  KDiccionarioTablasGUM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DiccionarioTablasGUM.ViewsDiccionarioTablasGUM.Views  4    .DiccionarioTablasGUM.Views.CamposTablasGUMView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:DiccionarioTablasGUM.Views     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  $��Avenir Light)�$��14(�  � Title$*  $Siesa - Diccionario de datos SIESAEE��$	��480��$	��800�� � 
ResizeMode$ CanResizeWithGrip=� � AllowsTransparency .  � WindowStyle$
 None=� � Icon$" /Assets/iconoSiesaCompBD.ICO�� � WindowStartupLocation$ CenterScreen=�.��� ���	#FFE0E0E0 � RowDefinitions �� $
��auto ��� $��* �.+�� -   $��0�����#0E4D96$��50��$	��Top=� � ColumnDefinitions �� $��8 ��� $
��auto ��� $��* ��� $
��auto ��� $��8 � �� $��8 ��� $��* ��� $��8 �.+��� -    ��Name$ ImagenLogoSiesa��$!��/Assets/logoSiesaBlanco.png��$��Center=�$
��Left=�$
��auto��$��2��$��1���� -   	 ��Name$	 	btnCerrar��$��3��$��1��$��Right=�$��5q�$��20��
������ $��0q�.���� 
�������$
��Bold"�x� $��1�� �� $��8 ��� $
��auto ��� $��* ��� $
��auto ��� $��8 � �� $��8 ��� $
��auto ��� $��* ��� $
��auto ��� $��8 �.+��� 
 �� Orientation$
 
Horizontal=�$��1��$��1��$��0,8,8,0q�.+��� $
��Bold"�$��Nombre de tabla: ���� -    ��Name$ PubStrNombreTablaGUM��"  FontAwesome.WPF.ImageAwesome  $��Right=�$��1��$��3��   Icon$ Circle=�$��21��$��21������  �� 
TargetType"   �� Triggers ��  �� Binding �� *PubIntindCambioEnTabla+ �� Value 0.a���  �� Property �@ 
Foreground �� Value
 � � ���  �@�ToolTip$* $Informaicion almacenada exitosamente����  �� *PubIntindCambioEnTabla+ 1.a���  �@ 
Foreground
 � �����  �@�ToolTip$ Informacion sin guardar����  �� *PubIntindCambioEnTabla+ 3.a���  �@ 
Foreground
 �  ����  �@�ToolTip$ Objeto Nuevo���� $��2��$��1��$��3��$��0,8,0,0q�.@��� $��Campos��.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ dtCamposGUM��   CanUserAddRows .     AutoGenerateColumns .  ���� *PubListCamposGUMActual+ �� SelectedItem �� *$"PubListTablasGUMCamposSeleccionada+   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��
 �������  � ��$ 0q� P�  P� Property �@ 
IsReadOnly P� Value . . ���  � �� �#F7F7F7P�  �@ 
IsSelected . . ���  � �� �#85C1E9   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  �@�ToolTip �� ���� *nombre+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    SortMemberPath$ 	indCambio��   
IsReadOnly .    CellTemplate �� .R�� .+��� 
������ $��0q�.��  $ Circle=�$��10��$��10������ "   ��  �� *	indCambio+ 0.a���  �@ 
Foreground
 � � ���  �@�ToolTip$* $Informaicion almacenada exitosamente����  �� *	indCambio+ 1.a���  �@ 
Foreground
 � �����  �@�ToolTip$ Informacion sin guardar����  �� *
indEsNuevo+ 1.a���  �@ 
Foreground
 �  ����  �@�ToolTip$ Objeto Nuevo��0  *System.Windows.Controls.DataGridTextColumn      Header$  Nombre Campo��$
 auto  . 1	  +System.Windows.Controls.DataGridBoundColumn! 	  Binding! �� *nombre+  $  Descripción��$
 auto ! �� *descripcion+"   	CellStyle" �� "   ��  �� *descripcion+  .a���  � �� �#EF274D  $  Notas��$
 auto ! �� *notas+" �� "   ��  �� *notas+  .a���  � �� �#EF274D  $  Orden de campo��$
 auto  . $ orden��#   SortDirection$# 	AscendingX�! �� *orden+  $  Orden de campo PK��$
 auto  . ! �� *	ordenPk+4
  .System.Windows.Controls.DataGridCheckBoxColumn
  $  Es identity��$
 auto  . ! �� *indIdentity+  $  Longitud��$
 auto  . ! �� *
longitud+  $  Orden identificados��$
 auto ! �� *ordenIdentificador+  $  	Precision��$
 auto  . ! �� *	presicion+  $   Orden de campo descripcion��$
 auto ! �� $ �� Path$$ ordenCampoDesc�  $  Tipo de dato en SQL��$
 auto  . ! �� *tipoDatoSql+
  $  Acepta Nulo��$
 auto  . ! �� *	indNulo+
  $  Configurable en GUM��$
 auto ! �� *indGumConfigurable+
  $  Sincronizable en GUM��$
 auto ! �� *indGumSincronizado+
  $  Sugerible en GUM��$
 auto ! �� *indGumSugerir+�� $��
Relaciones��.���  �� $	��23* ��� $
��111* ��� $
��644* �.+�  -   $ 
dtRelacGUM�� .   . $��3������ *PubListRelacCamposGUMActual+ �� "  .���  � ��
 �������  � ��$ 0q� P�  �@ 
IsReadOnly . . ���  � �� �#F7F7F7P�  �@ 
IsSelected . . ���  � �� �#85C1E9 �� "  .���  � {� .  ��  �� *
indOrden+ 2.a���  � {� .    $  Relacion��! �� $$ nombreRelacion�  $  Orden��! �� $$ indOrden�  $  Nombre tabla referenciada��! �� $$ nombreTablaRef�  $  Nombre campo��! �� $$ nombreCampo�  $  Nombre campo referenciado��! �� $$ nombreCampoRef��� $��Actualizacion��.��� .+�  -   $ dtCambiosCamposGUM�� .   .  ���� *PubListCambiosCampoGUMAtual+ �� "  .���  � ��
 �������  � ��$ 0q� P�  �@ 
IsReadOnly . . ���  � �� �#F7F7F7P�  �@ 
IsSelected . . ���  � ��
 ��  �   $  Nombre de Campo��! �� $$ campoModificados�  $  	Propiedad��! �� $$ 	propiedad�  $  Valor en Dicc. GUM��! �� $$ valorAnterior�  $  Valor en DB��! �� $$ 
valorNuevo��� $��3��$��1��$��2��$��0,8,0,0q�.+��  �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� -   $	 
btnPrimero��
�������
������ $��0q�$��30��$��60��$��60��$��0��$��0,0,8,0q�% � ToolTip$2% ,Desplazarse a los campos de la primera tabla��.���� $
 
Horizontal=�.+�  -	   $
��row1��$ ChevronLeft=�$��21��$��21��&   
Foreground& �#0E4D96  -
   $
��row2��$ ChevronLeft=�$��21��$��21��& �#0E4D96�� -   $	 btnAnterior��
������ $��0q�
�������$��30��$��30��$��30��$��1��$��0,0,8,0q�$3% -Desplazarse a los campos de la tabla anterior��.��  -   $��anterior��$ ChevronLeft=�$��21��$��21��& �#0E4D96�� -   $	 btnSiguiente��
�������
������ $��0q�$��30��$��30��$��30��$��2��$��0,0,8,0q�$4% .Desplazarse a los campos de la tabla siguiente��.��  -   $��	siguiente��$ ChevronRight=�$��21��$��21��& �#0E4D96�� -   $	 	btnUltimo��
�������
������ $��0q�$��30��$��60��$��60��$��3��$��0,0,8,0q�$1% +Desplazarse a los campos de la ultima tabla��.���� $
 
Horizontal=�.+�  -   $��ultimo1��$ ChevronRight=�$��21��$��21��& �#0E4D96  -   $��ultimo2��$ ChevronRight=�$��21��$��21��& �#0E4D96�� -   
�������   BtnStiloBotones#���   $��21��$	��100��$	��100��$��3��$��3��$��0,8,0,0q�$@% :Guarda los cambios realizados sobre los campos de la Tabla��.���� $
 
Horizontal=�.+�  $
 Save=�$��10��$��10��
& ������� $�� Guardar��