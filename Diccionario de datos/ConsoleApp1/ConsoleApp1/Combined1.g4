grammar Combined1;


 options {							
    language='CSharp2';								//lenguaje objetivo de la gramatica
}

/*
*	Reglas del Parser
*/
compileUnit:   
			   entradaInicial EOF?
			   ;
entradaInicial :
				   inicio codigo fin
				;
inicio:	 Simbolo 'START' dir
		;
codigo: ensamblaje codigo
		|
		ensamblaje
		;
dir:
	   Decimal
	   |
	   Hexadecimal
	   ;
ensamblaje:	Simbolo? (nemonico(dir|Simbolo) (',' 'X')? | 'RSUB') SL? EOF?
			|Simbolo? DirectivaB(CONSH|CONSC) SL? EOF?{System.Console.WriteLine($Simbolo);}
			|Simbolo? Directiva(Hexadecimal|Decimal) SL? EOF?{System.Console.WriteLine($Simbolo);}
			;
indexado: (',' 'X')
		;
fin: 'END' Simbolo? ;

nemonico: 'ADD'|'AND'|'COMP'|'DIV'|'J'|'RESW'|'JEQ'|'JGT'|'JLT'|'JSUB'|'LDA'|'LDCH'|'LDL'|'LDX'|'MUL'
				   |'OR'|'RD'|'STA'|'STCH'|'STL'|'STSW'|'STX'|'SUB'|'TD'|'TIX'|'WD'//Completar nemonicos
		;
operando returns[string op] : 
							(numero {$op=$numero.Num;}|
							Simbolo {$op=$Simbolo.text;})','?'X'?
							;
//Convert.ToString(int.Parse(line), 16);


numero returns[string Num]locals[int a] ://CHECAR CONVERSION DE DECIMAL
								Decimal{$Num=System.Convert.ToString(int.Parse($Decimal.text,System.Globalization.NumberStyles.HexNumber));}
								Hexadecimal{$Num=$Hexadecimal.text;}{System.Console.WriteLine($Hexadecimal);} 
								;


DirectivaB:
		'BYTE'
		;
Directiva: 'RESW' | 'RESB' | 'WORD' 
	   ;
CONSH:
		'X' '\''Hexadecimal'\''
		;

CONSC:
		'C' '\''Palabra'\''
		;
SL:
	  '\n'
	  ;
Decimal:([0-9])+;
Hexadecimal:([0-9A-Fa-f])+ ([Hh])?;
Simbolo:[A-Za-z]+;
Palabra:[A-Za-z]+;
WS
	: (' '|'\r'|'\n'|'\t')+ {Skip();}	//tokens que identifican las secuencas de escape.
	;
