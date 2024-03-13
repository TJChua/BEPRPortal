;#DropDB PRPortalEasyTest

#Application PRPortalWin
#Application PRPortalWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef PRPortalWin

*Action About...
 
*Action OK

#EndIf

#IfDef PRPortalWeb
*Action Navigation(My Details)
#EndIf
