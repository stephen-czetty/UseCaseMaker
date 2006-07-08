<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	xmlns:fox="http://xml.apache.org/fop/extensions">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" standalone="yes"/>
  <!-- Application parameters -->
  <xsl:param name="version"/>
  <!-- General parameters -->
  <xsl:param name="description"/>
  <xsl:param name="notes"/>
  <xsl:param name="relatedDocs"/>
  <xsl:param name="model"/>
  <xsl:param name="actor"/>
  <xsl:param name="useCase"/>
  <xsl:param name="package"/>
  <xsl:param name="actors"/>
  <xsl:param name="useCases"/>
  <xsl:param name="packages"/>
  <xsl:param name="summary"/>
  <xsl:param name="glossary"/>
  <xsl:param name="glossaryItem"/>
  <!-- 'Use case' specific paramters -->
  <xsl:param name="preconditions"/>
  <xsl:param name="postconditions"/>
  <xsl:param name="activeActors"/>
  <xsl:param name="primary"/>
  <xsl:param name="details"/>
  <xsl:param name="level"/>
  <xsl:param name="status"/>
  <xsl:param name="complexity"/>
  <xsl:param name="priority"/>
  <xsl:param name="implementation"/>
  <xsl:param name="release"/>
  <xsl:param name="assignedTo"/>
  <xsl:param name="openIssues"/>
  <xsl:param name="flowOfEvents"/>
  <xsl:param name="prose"/>
  <xsl:param name="requirements"/>
  <xsl:param name="history"/>
  <xsl:param name="implementationNodeSet"/>
  <xsl:param name="statusNodeSet"/>
  <xsl:param name="complexityNodeSet"/>
  <xsl:param name="levelNodeSet"/>
  <xsl:param name="historyTypeNodeSet"/>
  <!-- Start of Style Attributes -->
  <xsl:attribute-set name="UCMHTitle">
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">11pt</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="color">darkgray</xsl:attribute>
    <xsl:attribute name="text-align">right</xsl:attribute>
    <xsl:attribute name="space-after">0.1em</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="UCMHeader">
    <xsl:attribute name="border-bottom-width">0.3pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">groove</xsl:attribute>
    <xsl:attribute name="border-bottom-color">black</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="UCMFPageNumbering">
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">8pt</xsl:attribute>
    <xsl:attribute name="text-align">center</xsl:attribute>
    <xsl:attribute name="padding">0.5em</xsl:attribute>
    <xsl:attribute name="border-top-width">0.3pt</xsl:attribute>
    <xsl:attribute name="border-top-style">solid</xsl:attribute>
    <xsl:attribute name="border-top-color">black</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementTitleCell">
    <xsl:attribute name="background-color">#ACB8D0</xsl:attribute>
    <xsl:attribute name="border-top-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-top-style">solid</xsl:attribute>
    <xsl:attribute name="border-top-color">black</xsl:attribute>
    <xsl:attribute name="border-bottom-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">solid</xsl:attribute>
    <xsl:attribute name="border-bottom-color">black</xsl:attribute>
    <xsl:attribute name="border-left-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-left-style">solid</xsl:attribute>
    <xsl:attribute name="border-left-color">black</xsl:attribute>
    <xsl:attribute name="border-right-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-right-style">solid</xsl:attribute>
    <xsl:attribute name="border-right-color">black</xsl:attribute>
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">10.5pt</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="color">white</xsl:attribute>
    <xsl:attribute name="padding">0.2em</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementNameCell">
    <xsl:attribute name="background-color">white</xsl:attribute>
    <xsl:attribute name="border-top-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-top-style">solid</xsl:attribute>
    <xsl:attribute name="border-top-color">black</xsl:attribute>
    <xsl:attribute name="border-bottom-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">solid</xsl:attribute>
    <xsl:attribute name="border-bottom-color">black</xsl:attribute>
    <xsl:attribute name="border-left-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-left-style">solid</xsl:attribute>
    <xsl:attribute name="border-left-color">black</xsl:attribute>
    <xsl:attribute name="border-right-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-right-style">solid</xsl:attribute>
    <xsl:attribute name="border-right-color">black</xsl:attribute>
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">15pt</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="padding">0.2em</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementTextCell">
    <xsl:attribute name="border-bottom-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">solid</xsl:attribute>
    <xsl:attribute name="border-bottom-color">black</xsl:attribute>
    <xsl:attribute name="border-left-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-left-style">solid</xsl:attribute>
    <xsl:attribute name="border-left-color">black</xsl:attribute>
    <xsl:attribute name="border-right-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-right-style">solid</xsl:attribute>
    <xsl:attribute name="border-right-color">black</xsl:attribute>
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">9pt</xsl:attribute>
    <xsl:attribute name="padding">0.2em</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementLinkCell">
    <xsl:attribute name="border-bottom-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">solid</xsl:attribute>
    <xsl:attribute name="border-bottom-color">black</xsl:attribute>
    <xsl:attribute name="border-left-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-left-style">solid</xsl:attribute>
    <xsl:attribute name="border-left-color">black</xsl:attribute>
    <xsl:attribute name="border-right-width">0.2pt</xsl:attribute>
    <xsl:attribute name="border-right-style">solid</xsl:attribute>
    <xsl:attribute name="border-right-color">black</xsl:attribute>
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">9pt</xsl:attribute>
    <xsl:attribute name="padding">0.2em</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementLink">
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">9pt</xsl:attribute>
    <xsl:attribute name="font-style">italic</xsl:attribute>
    <xsl:attribute name="text-decoration">underline</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ParaSep">
    <xsl:attribute name="space-after">12pt</xsl:attribute>
    <xsl:attribute name="space-before">12pt</xsl:attribute>
    <xsl:attribute name="border-bottom-width">0.3pt</xsl:attribute>
    <xsl:attribute name="border-bottom-style">solid</xsl:attribute>
    <xsl:attribute name="border-bottom-color">gray</xsl:attribute>
    <xsl:attribute name="margin-left">0.5cm</xsl:attribute>
    <xsl:attribute name="margin-right">0.5cm</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ParaTitle">
    <xsl:attribute name="text-align">left</xsl:attribute>
    <xsl:attribute name="font-family">sans-serif</xsl:attribute>
    <xsl:attribute name="font-size">15pt</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="space-after.optimum">1.0em</xsl:attribute>
  </xsl:attribute-set>
  <!-- End of Style Attributes -->
	
	<!-- Start of template -->
  <xsl:template match="/">
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:fox="http://xml.apache.org/fop/extensions">
      <fo:layout-master-set>
        <fo:simple-page-master master-name="all-pages"
                        page-width="210mm" page-height="297mm"
						margin-top="10mm" margin-bottom="10mm"
        				margin-left="15mm" margin-right="15mm">
          <fo:region-before region-name="xsl-region-before" extent="15mm"/>
          <fo:region-body region-name="xsl-region-body" margin-top="20mm" margin-bottom="20mm"/>
          <fo:region-after region-name="xsl-region-after" extent="5mm"/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      <fo:page-sequence master-reference="all-pages">
		<!-- Header definition -->
        <fo:static-content flow-name="xsl-region-before">
          <fo:block xsl:use-attribute-sets="UCMHeader">
            <fo:table table-layout="fixed" width="18cm">
              <fo:table-column column-number="1" column-width="15cm"/>
              <fo:table-column column-number="2" column-width="3cm"/>
              <fo:table-body>
                <fo:table-row>
                  <fo:table-cell column-number="1"/>
                  <fo:table-cell column-number="2">
                    <fo:block xsl:use-attribute-sets="UCMHTitle">Use Case Maker
                      <xsl:value-of select="$version"/>
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>
              </fo:table-body>
            </fo:table>
          </fo:block>
        </fo:static-content>
        <!-- Footer definition -->
        <fo:static-content flow-name="xsl-region-after">
          <fo:block xsl:use-attribute-sets="UCMFPageNumbering">
            <fo:page-number/>
          </fo:block>
        </fo:static-content>
        <!-- Body definition -->
        <fo:flow flow-name="xsl-region-body">
          <xsl:apply-templates/>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>
  </xsl:template>
  <!-- Start of Summary section -->
  <xsl:template name="MakeSummary">
    <fo:block xsl:use-attribute-sets="ParaTitle">
      <xsl:value-of select="$summary"/>
    </fo:block>
    <xsl:call-template name="MakeSummaryItem">
      <xsl:with-param name="node" select="//Model"/>
      <xsl:with-param name="indent" select="0"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="MakeSummaryItem">
    <xsl:param name="node"/>
    <xsl:param name="indent"/>
    <xsl:call-template name="FormatSummaryItem">
      <xsl:with-param name="node" select="$node"/>
      <xsl:with-param name="indent" select="$indent"/>
    </xsl:call-template>
    <xsl:for-each select="$node/Actors/Actor">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 0.5"/>
      </xsl:call-template>
    </xsl:for-each>
    <xsl:for-each select="$node/UseCases/UseCase">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 0.5"/>
      </xsl:call-template>
    </xsl:for-each>
    <xsl:for-each select="$node/Packages/Package">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 0.5"/>
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="FormatSummaryItem">
    <xsl:param name="node"/>
    <xsl:param name="indent"/>
    
	<xsl:element name="fo:block">
      <xsl:attribute name="margin-left">
        <xsl:value-of select="concat(string($indent),'cm')" />
      </xsl:attribute>
      <xsl:attribute name="space-after">0.5em</xsl:attribute>
      <xsl:attribute name="font-size">9pt</xsl:attribute>
		<xsl:element name="fo:table">
		<xsl:attribute name="table-layout">fixed</xsl:attribute>
		<xsl:attribute name="width">18.0cm</xsl:attribute>
		<xsl:element name="fo:table-column">
			<xsl:attribute name="column-number">1</xsl:attribute>
			<xsl:attribute name="column-width">
			  <xsl:value-of select="concat(17.0 - $indent,'cm')" />
			</xsl:attribute>
		</xsl:element>
		<xsl:element name="fo:table-column">
			<xsl:attribute name="column-number">2</xsl:attribute>
			<xsl:attribute name="column-width">1.0cm</xsl:attribute>
		</xsl:element>
		<fo:table-body>
			<fo:table-row>
			<fo:table-cell column-number="1" border-bottom-width="0.3pt" border-bottom-style="solid" border-bottom-color="black">
				<fo:block>
				<xsl:element name="fo:basic-link">
				<xsl:attribute name="internal-destination">
					<xsl:value-of select="$node/@UniqueID"/>
				</xsl:attribute>
					<xsl:value-of select="concat($node/@Name,' (',$node/@Prefix,$node/@ID,') ')"/>
				</xsl:element>
				</fo:block>  			
			</fo:table-cell>
			<fo:table-cell column-number="2" border-bottom-width="0.3pt" border-bottom-style="solid" border-bottom-color="black">
				<fo:block text-align="end">
				<xsl:element name="fo:page-number-citation">
				<xsl:attribute name="ref-id">
					<xsl:value-of select="$node/@UniqueID"/>
				</xsl:attribute>
				</xsl:element>
				</fo:block>
			</fo:table-cell>
			</fo:table-row>
		</fo:table-body>
		</xsl:element>
	</xsl:element>
    <!--
      <xsl:element name="fo:block">
        <xsl:attribute name="font-size">9pt</xsl:attribute>
        <xsl:attribute name="text-align-last">justify</xsl:attribute>
        <xsl:attribute name="start-indent">
          <xsl:value-of select="concat(string($indent),'cm')"/>
        </xsl:attribute>
        <xsl:attribute name="space-after">
          <xsl:value-of select="string('0.5em')"/>
        </xsl:attribute>
        <xsl:element name="fo:basic-link">
          <xsl:attribute name="internal-destination">
            <xsl:value-of select="$node/@UniqueID"/>
          </xsl:attribute>
            <xsl:value-of select="concat($node/@Name,' (',$node/@Prefix,$node/@ID,') ')"/>
        </xsl:element>
        <fo:leader leader-pattern="dots"/>
        <xsl:element name="fo:page-number-citation">
          <xsl:attribute name="ref-id">
            <xsl:value-of select="$node/@UniqueID"/>
          </xsl:attribute>
        </xsl:element>
      </xsl:element>
    -->
  </xsl:template>
  <!-- End of Summary section -->
    
    <!-- Start of Glossary section -->
  <xsl:template match="Glossary">
    <fo:block xsl:use-attribute-sets="ParaTitle">
      <xsl:value-of select="$glossary"/>
    </fo:block>
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="6cm"/>
      <fo:table-column column-number="2" column-width="12cm"/>
      <fo:table-body>
        <fo:table-row>
          <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTitleCell">
            <fo:block>
              <xsl:value-of select="$glossaryItem"/>
            </fo:block>
          </fo:table-cell>
          <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTitleCell">
            <fo:block>
              <xsl:value-of select="$description"/>
            </fo:block>
          </fo:table-cell>
        </fo:table-row>
        <xsl:apply-templates select="GlossaryItem"/>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <xsl:template match="GlossaryItem">
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <xsl:element name="fo:block">
          <xsl:attribute name="id">
            <xsl:value-of select="@UniqueID"/>
          </xsl:attribute>
          <xsl:value-of select="@Name"/>
        </xsl:element>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="Description"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- End of Glossary section -->
  <xsl:template match="Model">
	<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$model"/>
    </xsl:call-template>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
    <!-- Actors list -->
    <xsl:if test="Actors/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$actors"/>
          </xsl:call-template>
          <xsl:for-each select="Actors/Actor">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Use cases list -->
    <xsl:if test="UseCases/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$useCases"/>
          </xsl:call-template>
          <xsl:for-each select="UseCases/UseCase">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Packages list -->
    <xsl:if test="Packages/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$packages"/>
          </xsl:call-template>
          <xsl:for-each select="Packages/Package">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Requirements details -->
    <xsl:if test="Requirements/*">
      <xsl:apply-templates select="Requirements"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>     
    <!-- Actors details -->
    <xsl:apply-templates select="Actors/Actor"/>
    <!-- Use cases details -->
    <xsl:apply-templates select="UseCases/UseCase"/>
    <fo:block break-after="page"/>
    <!-- Recurse package items -->
    <xsl:apply-templates select="Packages/Package"/>
    <!-- Glossary -->
    <xsl:apply-templates select="Glossary"/>
    <fo:block break-after="page"/>
    <!-- Summary -->
    <xsl:call-template name="MakeSummary"/>
  </xsl:template>
  <xsl:template match="Package">
		<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$package"/>
    </xsl:call-template>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
    <!-- Actors list -->
    <xsl:if test="Actors/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$actors"/>
          </xsl:call-template>
          <xsl:for-each select="Actors/Actor">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Use cases list -->
    <xsl:if test="UseCases/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$useCases"/>
          </xsl:call-template>
          <xsl:for-each select="UseCases/UseCase">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Packages list -->
    <xsl:if test="Packages/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$packages"/>
          </xsl:call-template>
          <xsl:for-each select="Packages/Package">
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
              <xsl:with-param name="link" select="@UniqueID"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Requirements details -->
    <xsl:if test="Requirements/*">
      <xsl:apply-templates select="Requirements"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>    
    <!-- Actors details -->
    <xsl:apply-templates select="Actors/Actor"/>
    <!-- Use cases details -->
    <xsl:apply-templates select="UseCases/UseCase"/>
    <fo:block break-after="page"/>
    <xsl:apply-templates select="Packages/Package"/>
  </xsl:template>
  <!-- Actor details -->
  <xsl:template match="Actor">
		<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$actor"/>
    </xsl:call-template>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
  </xsl:template>
  <!-- Use case details -->
  <xsl:template match="UseCase">
		<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$useCase"/>
    </xsl:call-template>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
    <!-- Use case specific -->
    <xsl:call-template name="General"/>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
    <xsl:call-template name="Details"/>
    <xsl:if test="OpenIssues/*">
      <xsl:apply-templates select="OpenIssues"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Steps/*">
      <xsl:apply-templates select="Steps"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Prose/text() != ''">
      <xsl:apply-templates select="Prose"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="HistoryItems/*">
      <xsl:apply-templates select="HistoryItems"/>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
  </xsl:template>
  <!-- 'Use case' general -->
  <xsl:template name="General">
    <xsl:if test="Preconditions/text()">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$preconditions"/>
          </xsl:call-template>
          <xsl:call-template name="TextRow">
            <xsl:with-param name="text" select="Preconditions/text()"/>
          </xsl:call-template>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Postconditions/text()">
      <fo:block space-after="12pt"/>
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$postconditions"/>
          </xsl:call-template>
          <xsl:call-template name="TextRow">
            <xsl:with-param name="text" select="Postconditions/text()"/>
          </xsl:call-template>
        </fo:table-body>
      </fo:table>
      <fo:block xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="ActiveActors/*">
      <xsl:apply-templates select="ActiveActors"/>
    </xsl:if>
  </xsl:template>
  <!-- 'Use case' details -->
  <xsl:template name="Details">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="6cm"/>
      <fo:table-column column-number="2" column-width="12cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="2"/>
          <xsl:with-param name="text" select="$details"/>
        </xsl:call-template>
        <xsl:apply-templates select="Priority"/>
        <xsl:apply-templates select="Level"/>
        <xsl:apply-templates select="Complexity"/>
        <xsl:apply-templates select="Status"/>
        <xsl:apply-templates select="Implementation"/>
        <xsl:if test="AssignedTo/text() != ''">
          <xsl:apply-templates select="AssignedTo"/>
        </xsl:if>
        <xsl:if test="Release/text() != ''">
          <xsl:apply-templates select="Release"/>
        </xsl:if>
      </fo:table-body>
    </fo:table>
    <fo:block xsl:use-attribute-sets="ParaSep"/>
  </xsl:template>
  <!-- 'Use case' priority -->
  <xsl:template match="Priority">
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$priority"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="text()"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' level -->
  <xsl:template match="Level">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$levelNodeSet[@EnumName = $target]"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$level"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$value"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' complexity -->
  <xsl:template match="Complexity">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$complexityNodeSet[@EnumName = $target]"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$complexity"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$value"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' status -->
  <xsl:template match="Status">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$statusNodeSet[@EnumName = $target]"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$status"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$value"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' implementation -->
  <xsl:template match="Implementation">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$implementationNodeSet[@EnumName = $target]"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$implementation"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$value"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' assigned to -->
  <xsl:template match="AssignedTo">
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$assignedTo"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="text()"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' release -->
  <xsl:template match="Release">
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$release"/>
        </fo:block>
      </fo:table-cell>
      <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="text()"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <!-- 'Use case' active actors -->
  <xsl:template match="ActiveActors">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="18cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$activeActors"/>
        </xsl:call-template>
        <xsl:for-each select="ActiveActor">
          <xsl:variable name="target" select="ActorUniqueID/text()"/>
          <xsl:choose>
            <xsl:when test="IsPrimary/text() = 'True'">
              <xsl:call-template name="IntLinkRow">
                <xsl:with-param name="text" select="concat(//Actor[@UniqueID = $target]/@Name,' (',$primary,')')"/>
                <xsl:with-param name="link" select="$target"/>
              </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
              <xsl:call-template name="IntLinkRow">
                <xsl:with-param name="text" select="//Actor[@UniqueID = $target]/@Name"/>
                <xsl:with-param name="link" select="$target"/>
              </xsl:call-template>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- 'Use case' open issues -->
  <xsl:template match="OpenIssues">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="3.5cm"/>
      <fo:table-column column-number="2" column-width="14.5cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="2"/>
          <xsl:with-param name="text" select="$openIssues"/>
        </xsl:call-template>
        <xsl:for-each select="OpenIssue">
          <fo:table-row>
            <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:value-of select="Name"/>
              </fo:block>
            </fo:table-cell>
            <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="Description"/>
                </xsl:call-template>
              </fo:block>
            </fo:table-cell>
          </fo:table-row>
        </xsl:for-each>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- 'Use case' flow of events -->
  <xsl:template match="Steps">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="3.5cm"/>
      <fo:table-column column-number="2" column-width="14.5cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="2"/>
          <xsl:with-param name="text" select="$flowOfEvents"/>
        </xsl:call-template>
        <xsl:for-each select="Step">
          <fo:table-row>
            <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:value-of select="Name"/>
              </fo:block>
            </fo:table-cell>
            <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="Description"/>
                </xsl:call-template>
              </fo:block>
            </fo:table-cell>
          </fo:table-row>
        </xsl:for-each>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- 'Use case' prose -->
  <xsl:template match="Prose">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="18cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$prose"/>
        </xsl:call-template>
        <fo:table-row>
          <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
            <fo:block>
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="text()"/>
              </xsl:call-template>
            </fo:block>
          </fo:table-cell>
        </fo:table-row>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- 'Use case' requirements -->
  <xsl:template match="Requirements">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="3.5cm"/>
      <fo:table-column column-number="2" column-width="14.5cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="2"/>
          <xsl:with-param name="text" select="$requirements"/>
        </xsl:call-template>
        <xsl:for-each select="Requirement">
          <fo:table-row>
            <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:value-of select="Name"/>
              </fo:block>
            </fo:table-cell>
            <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="Description"/>
                </xsl:call-template>
              </fo:block>
            </fo:table-cell>
          </fo:table-row>
        </xsl:for-each>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- 'Use case' history -->
  <xsl:template match="HistoryItems">
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="3.5cm"/>
      <fo:table-column column-number="2" column-width="3.5cm"/>
      <fo:table-column column-number="3" column-width="3.5cm"/>
      <fo:table-column column-number="4" column-width="7.5cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="4"/>
          <xsl:with-param name="text" select="$history"/>
        </xsl:call-template>
        <xsl:for-each select="HistoryItem">
          <fo:table-row>
            <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:value-of select="substring-before(DateValue,' ')"/>
              </fo:block>
            </fo:table-cell>
            <fo:table-cell column-number="2" xsl:use-attribute-sets="ElementTextCell">
              <xsl:variable name="typeTarget" select="Type/text()"/>
              <xsl:variable name="typeValue" select="$historyTypeNodeSet[@EnumName = $typeTarget]"/>
              <fo:block>
                <xsl:value-of select="$typeValue"/>
              </fo:block>
            </fo:table-cell>
            <xsl:variable name="actionTarget" select="Action/text()"/>
            <xsl:choose>
              <xsl:when test="Type/text() = 'Status'">
                <fo:table-cell column-number="3" xsl:use-attribute-sets="ElementTextCell">
                  <xsl:variable name="statusValue" select="$statusNodeSet[@ListIndex = $actionTarget]"/>
                  <fo:block>
                    <xsl:value-of select="$statusValue"/>
                  </fo:block>
                </fo:table-cell>
              </xsl:when>
              <xsl:otherwise>
                <fo:table-cell column-number="3" xsl:use-attribute-sets="ElementTextCell">
                  <xsl:variable name="implValue" select="$implementationNodeSet[@ListIndex = $actionTarget]"/>
                  <fo:block>
                    <xsl:value-of select="$implValue"/>
                  </fo:block>
                </fo:table-cell>
              </xsl:otherwise>
            </xsl:choose>
            <fo:table-cell column-number="4" xsl:use-attribute-sets="ElementTextCell">
              <fo:block>
                <xsl:value-of select="Notes"/>
              </fo:block>
            </fo:table-cell>
          </fo:table-row>
        </xsl:for-each>
      </fo:table-body>
    </fo:table>
  </xsl:template>
  <!-- General common attributes -->
  <xsl:template name="CommonAttributes">
    <xsl:param name="node"/>
    <xsl:param name="elementType"/>
    <fo:table table-layout="fixed" width="18cm">
      <fo:table-column column-number="1" column-width="18cm"/>
      <fo:table-body>
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$elementType"/>
        </xsl:call-template>
        <xsl:call-template name="NameRow">
          <xsl:with-param name="text" select="concat($node/@Name,' (',$node/@Prefix,$node/@ID,')')"/>
          <xsl:with-param name="uniqueID" select="$node/@UniqueID"/>
        </xsl:call-template>
      </fo:table-body>
    </fo:table>
    <fo:block space-after="12pt"/>
    <xsl:if test="$node/Attributes/Description/text() != ''">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$description"/>
          </xsl:call-template>
          <xsl:call-template name="TextRow">
            <xsl:with-param name="text" select="$node/Attributes/Description/text()"/>
          </xsl:call-template>
        </fo:table-body>
      </fo:table>
      <fo:block space-after="12pt"/>
    </xsl:if>
    <xsl:if test="$node/Attributes/Notes/text() != ''">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$notes"/>
          </xsl:call-template>
          <xsl:call-template name="TextRow">
            <xsl:with-param name="text" select="$node/Attributes/Notes/text()"/>
          </xsl:call-template>
        </fo:table-body>
      </fo:table>
      <fo:block space-after="12pt"/>
    </xsl:if>
    <xsl:if test="$node/Attributes/RelatedDocuments/*">
      <fo:table table-layout="fixed" width="18cm">
        <fo:table-column column-number="1" column-width="18cm"/>
        <fo:table-body>
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$relatedDocs"/>
          </xsl:call-template>
          <xsl:for-each select="$node/Attributes/RelatedDocuments/RelatedDocument">
            <xsl:call-template name="ExtLinkRow">
              <xsl:with-param name="text" select="FileName/text()"/>
              <xsl:with-param name="link" select="concat('file:///',translate(FileName/text(),'\\','/'))"/>
            </xsl:call-template>
          </xsl:for-each>
        </fo:table-body>
      </fo:table>
    </xsl:if>
  </xsl:template>
  <xsl:template name="HeaderRow">
    <xsl:param name="colspan" select="1"/>
    <xsl:param name="text"/>
    <fo:table-row>
      <xsl:element name="fo:table-cell" use-attribute-sets="ElementTitleCell">
        <xsl:attribute name="column-number">1</xsl:attribute>
        <xsl:attribute name="number-columns-spanned">
          <xsl:value-of select="$colspan"/>
        </xsl:attribute>
        <fo:block>
          <xsl:value-of select="$text"/>
        </fo:block>
      </xsl:element>
    </fo:table-row>
  </xsl:template>
  <xsl:template name="NameRow">
    <xsl:param name="text"/>
    <xsl:param name="uniqueID"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementNameCell">
        <xsl:element name="fo:block">
          <xsl:attribute name="id">
            <xsl:value-of select="$uniqueID"/>
          </xsl:attribute>
          <xsl:value-of select="$text"/>
        </xsl:element>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <xsl:template name="TextRow">
    <xsl:param name="text"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementTextCell">
        <fo:block>
          <xsl:value-of select="$text"/>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <xsl:template name="ExtLinkRow">
    <xsl:param name="text"/>
    <xsl:param name="link"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementLinkCell">
        <fo:block>
          <xsl:element name="fo:basic-link" use-attribute-sets="ElementLink">
            <xsl:attribute name="external-destination">
              <xsl:value-of select="$link"/>
            </xsl:attribute>
            <xsl:value-of select="$text"/>
          </xsl:element>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <xsl:template name="IntLinkRow">
    <xsl:param name="text"/>
    <xsl:param name="link"/>
    <fo:table-row>
      <fo:table-cell column-number="1" xsl:use-attribute-sets="ElementLinkCell">
        <fo:block>
          <xsl:element name="fo:basic-link" use-attribute-sets="ElementLink">
            <xsl:attribute name="internal-destination">
              <xsl:value-of select="$link"/>
            </xsl:attribute>
            <xsl:value-of select="$text"/>
          </xsl:element>
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:template>
  <xsl:template name="MatchLink">
    <xsl:param name="text"/>
    <xsl:choose>
      <xsl:when test="contains($text,'&#34;') and string-length($text) > 1">
        <xsl:variable name="start" select="substring-after($text, '&#34;')" />
        <xsl:variable name="end" select="substring-before($start, '&#34;')" />
        <xsl:choose>
          <xsl:when test="$end">
            <xsl:if test="$start">
              <xsl:value-of select="substring-before($text,$end)"/>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="//Glossary/*[@Name = $end]">
                <xsl:call-template name="MakeAnchorLink">
                  <xsl:with-param name="glossary_uid" select="//Glossary/@UniqueID"/>
                  <xsl:with-param name="text" select="$end"/>
                  <xsl:with-param name="node" select="//Glossary/*[@Name = $end]"/>
                </xsl:call-template>
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                </xsl:call-template>
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="//*[@Path = $end]">
                    <xsl:call-template name="MakePathLink">
                      <xsl:with-param name="text" select="$end"/>
                      <xsl:with-param name="node" select="//*[@Path = $end]"/>
                    </xsl:call-template>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                    </xsl:call-template>
                  </xsl:when>
                  <xsl:when test="//*[@Name = $end]">
                    <xsl:call-template name="MakeNameLink">
                      <xsl:with-param name="text" select="$end"/>
                      <xsl:with-param name="node" select="//*[@Name = $end]"/>
                    </xsl:call-template>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                    </xsl:call-template>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="$end"/>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,$end)"/>
                    </xsl:call-template>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="MatchLink">
              <xsl:with-param name="text" select="substring-after($text,$start)"/>
            </xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="MakeNameLink">
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:element name="fo:basic-link" use-attribute-sets="ElementLink">
      <xsl:attribute name="internal-destination">
        <xsl:value-of select="$node/@UniqueID"/>
      </xsl:attribute>
      <xsl:value-of select="$text"/>
    </xsl:element>
    <xsl:value-of select="'&#34;'" />
  </xsl:template>
  <xsl:template name="MakePathLink">
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:element name="fo:basic-link" use-attribute-sets="ElementLink">
      <xsl:attribute name="internal-destination">
        <xsl:value-of select="$node/@UniqueID"/>
      </xsl:attribute>
      <xsl:value-of select="$text"/>
    </xsl:element>
    <xsl:value-of select="'&#34;'" />
  </xsl:template>
  <xsl:template name="MakeAnchorLink">
    <xsl:param name="glossary_uid"/>
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:element name="fo:basic-link" use-attribute-sets="ElementLink">
      <xsl:attribute name="internal-destination">
        <xsl:value-of select="$node/@UniqueID"/>
      </xsl:attribute>
      <xsl:value-of select="$text"/>
    </xsl:element>
    <xsl:value-of select="'&#34;'" />
  </xsl:template>
</xsl:stylesheet>