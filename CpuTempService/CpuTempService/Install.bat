:: -- WARNING --
:: Please save this file with encoding <ANSI> or <UTF-8 without BOM>.

%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil.exe CpuTempService.exe
Net Start CpuTempService
sc config CpuTempService start= auto