<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:import href="CommonTags.xsl" />
	<xsl:output method="html" />
	
	<xsl:param name="elementUniqueID" />	
	<xsl:param name="elementType" />
	<xsl:param name="description" />
	<xsl:param name="notes" />
	<xsl:param name="relatedDocs" />

	<xsl:template match="/">
		<html>
			<head>
				<link rel="stylesheet" type="text/css" href="ucm.css" />
			</head>
			<body>
				<xsl:apply-templates mode="page" select="//*[@UniqueID = $elementUniqueID]" />
			</body>
		</html>
	</xsl:template>

	<xsl:template mode="page" match="*">
		<xsl:call-template name="CommonTags">
				<xsl:with-param name="elementType">$elementType</xsl:with-param>
				<xsl:with-param name="description">$description</xsl:with-param>
				<xsl:with-param name="notes">$notes</xsl:with-param>
				<xsl:with-param name="relatedDocs">$relatedDocs</xsl:with-param>
		</xsl:call-template>
	</xsl:template>
</xsl:stylesheet>