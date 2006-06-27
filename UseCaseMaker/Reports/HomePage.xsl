<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	
	<xsl:param name="version" />
	<xsl:param name="copyright" />

	<xsl:template match="/">
		<html>
			<head>
				<title></title>
				<link rel="stylesheet" type="text/css" href="ucm.css" />
			</head>
			<body>
				<table border="1" bgcolor="white" cellpadding="5" cellspacing="0" align="center" width="500px">
					<tr>
						<td align="center">
							<img class="TransparentTableCell" src="Mago_logo.gif" />
							<hr />
							<p class="Title"><u>Use Case Maker</u></p>
							<p class="SubTitle"><xsl:value-of select="$version" /></p>
							<p><font size="-1"><xsl:value-of select="$copyright" /></font></p>
							<a href="http://www.magosoftware.com"><font size="-1">www.magosoftware.com</font></a>
						</td>
					</tr>
				</table>
			</body>
		</html>		
	</xsl:template>
</xsl:stylesheet>
  