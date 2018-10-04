grammar function;

options
{
	language = CSharp2;
}
//@parser::namespace { Generated }
//@lexer::namespace  { Generated }

/*
 * Parser Rules
 */

 @header{
	using System.Collections;
}

@members{
	public double Var{
		private get;
		set;
	}

	public double Ans{
		get;
		private set;
	}
}

function :	expr { Ans = $expr.obj; }
		| EOF;

expr	returns[double obj]	
		:	'('expr')' {$obj = $expr.obj;}
		|	op1 = expr pow op2 = expr {$obj =  Math.Pow($op1.obj, $op2.obj);}
		|	op1 = expr mul op2 = expr {$obj =  $op1.obj * $op2.obj;}
		|	op1 = expr div op2 = expr {$obj =  $op1.obj / $op2.obj;}
		|	op1 = expr add op2 = expr {$obj =  $op1.obj + $op2.obj;}
		|	op1 = expr sub op2 = expr {$obj =  $op1.obj - $op2.obj;}
		|	math_func {$obj = $math_func.obj;}
		|	value {$obj = $value.obj;}
		;

skip: ;

mul: '*';

div: '/';

pow: '^';

add: '+';

sub: '-';

value	returns[double obj]	
		:	NUMBER	{$obj =  double.Parse($NUMBER.text);}
		|	VAR {$obj = Var;}
		|	PI {$obj = Math.PI;}
		;

math_func	returns[double obj]	
			:	cos {$obj = $cos.obj;}
			|	sin {$obj = $sin.obj;}
			|	tan {$obj = $tan.obj;}
			|	cot {$obj = $cot.obj;}
			|	exponent {$obj = $exponent.obj;}
			|	sqrt {$obj = $sqrt.obj;}
			|	abs {$obj = $abs.obj;}
			|	acos {$obj = $acos.obj;}
			|	asin {$obj = $asin.obj;}
			|	atan {$obj = $atan.obj;}
			|	cosh {$obj = $cosh.obj;}
			|	sinh {$obj = $sinh.obj;}
			|	tanh {$obj = $tanh.obj;}
			|	ln {$obj = $ln.obj;}
			|	log {$obj = $log.obj;}
			;

cos	returns[double obj]	
	: 'cos' '(' expr ')' {$obj = Math.Cos($expr.obj);}
	;

sin	returns[double obj]	
	: 'sin' '('expr')' {$obj = Math.Sin($expr.obj);}
	;

tan	returns[double obj]	
	: 'tan' '('expr')' {$obj = Math.Tan($expr.obj);}
	;

cot	returns[double obj]	
	: 'cot' '(' expr')' {$obj = 1f / Math.Tan($expr.obj);}
	;

exponent	returns[double obj]	
		: 'e' pow expr {$obj = Math.Exp($expr.obj);}
		| 'exp' '('expr')' {$obj = Math.Exp($expr.obj);}
		;

sqrt	returns[double obj]	
	: 'sqrt' '('expr')' {$obj = Math.Sqrt($expr.obj);}
	;

abs	returns[double obj]	
	: 'abs' '('expr')' {$obj = Math.Abs($expr.obj);}
	;

acos	returns[double obj]	
	: 'acos' '(' expr ')' {$obj = Math.Acos($expr.obj);}
	;

asin	returns[double obj]	
	: 'asin' '(' expr ')' {$obj = Math.Asin($expr.obj);}
	;

atan	returns[double obj]	
	: 'atan' '(' expr ')' {$obj = Math.Atan($expr.obj);}
	;

cosh	returns[double obj]	
	: 'cosh' '(' expr ')' {$obj = Math.Cosh($expr.obj);}
	;

sinh	returns[double obj]	
	: 'sinh' '(' expr ')' {$obj = Math.Sinh($expr.obj);}
	;

tanh	returns[double obj]	
	: 'tanh' '(' expr ')' {$obj = Math.Tanh($expr.obj);}
	;

ln	returns[double obj]	
	: 'ln' '(' expr ')' {$obj = Math.Log($expr.obj);}
	;

log	returns[double obj]	
	: 'log' '_' '{' NUMBER '}' '(' expr ')' {$obj = Math.Log($expr.obj,double.Parse($NUMBER.text));}
	;


/*
 * Lexer Rules
 */
VAR: [x,t];
fragment INT: '-'?[0-9]+;
fragment FLOAT: '-'?INT+ ([,] INT+)?;
 PI: ('\u03C0'|'pi');
 NUMBER:	INT 
		|FLOAT 
		;
WHITESPACE          : (' '|'\t'|'\r'|'\n')+ -> skip ;
