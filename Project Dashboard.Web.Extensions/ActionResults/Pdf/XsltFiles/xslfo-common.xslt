<?xml version="1.0" encoding="UTF-8"?>
<!--<!DOCTYPE xsl:stylesheet>-->
<!-- ============================================
    This stylesheet transforms some common html
    elements into xsl-fo elements.
    
    By Neil Cumpstey @ Zone, including some items
    taken from a stylesheet with a similar purpose
    from developerWorks
    =============================================== -->
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fo="http://www.w3.org/1999/XSL/Format"
    xmlns:dash.common="urn:dash.common"
    exclude-result-prefixes="dash.common">

  <xsl:param name="root-url" />
  <xsl:param name="highlight-colour">#424297</xsl:param>
  <xsl:param name="link-colour">#ee3d96</xsl:param>
  <xsl:param name="header-left">Zone</xsl:param>
  <xsl:param name="header-right">thisiszone.com</xsl:param>
  <xsl:param name="default-font-size">8</xsl:param>
  <xsl:param name="default-vertical-spacing-mm" select="$default-font-size * 0.125" />
  <xsl:param name="default-list-indent-mm">5</xsl:param>
  <xsl:param name="default-list-label-spacing-mm">5</xsl:param>
  <xsl:param name="page-width">210</xsl:param>
  <xsl:param name="include-images" select="boolean('false')" />

  <xsl:template match="/">
    <xsl:apply-templates select="html" />
  </xsl:template>

  <xsl:template match="html">
    <xsl:text disable-output-escaping="yes">
&lt;!DOCTYPE fo:root [
  &lt;!ENTITY tilde  "&amp;#126;"&gt;
  &lt;!ENTITY florin "&amp;#131;"&gt;
  &lt;!ENTITY elip   "&amp;#133;"&gt;
  &lt;!ENTITY dag    "&amp;#134;"&gt;
  &lt;!ENTITY ddag   "&amp;#135;"&gt;
  &lt;!ENTITY cflex  "&amp;#136;"&gt;
  &lt;!ENTITY permil "&amp;#137;"&gt;
  &lt;!ENTITY uscore "&amp;#138;"&gt;
  &lt;!ENTITY OElig  "&amp;#140;"&gt;
  &lt;!ENTITY lsquo  "&amp;#145;"&gt;
  &lt;!ENTITY rsquo  "&amp;#146;"&gt;
  &lt;!ENTITY ldquo  "&amp;#147;"&gt;
  &lt;!ENTITY rdquo  "&amp;#148;"&gt;
  &lt;!ENTITY bullet "&amp;#149;"&gt;
  &lt;!ENTITY endash "&amp;#150;"&gt;
  &lt;!ENTITY emdash "&amp;#151;"&gt;
  &lt;!ENTITY trade  "&amp;#153;"&gt;
  &lt;!ENTITY oelig  "&amp;#156;"&gt;
  &lt;!ENTITY Yuml   "&amp;#159;"&gt;
  &lt;!ENTITY nbsp   "&amp;#160;"&gt;
  &lt;!ENTITY iexcl  "&amp;#161;"&gt;
  &lt;!ENTITY cent   "&amp;#162;"&gt;
  &lt;!ENTITY pound  "&amp;#163;"&gt;
  &lt;!ENTITY curren "&amp;#164;"&gt;
  &lt;!ENTITY yen    "&amp;#165;"&gt;
  &lt;!ENTITY brvbar "&amp;#166;"&gt;
  &lt;!ENTITY sect   "&amp;#167;"&gt;
  &lt;!ENTITY uml    "&amp;#168;"&gt;
  &lt;!ENTITY copy   "&amp;#169;"&gt;
  &lt;!ENTITY ordf   "&amp;#170;"&gt;
  &lt;!ENTITY laquo  "&amp;#171;"&gt;
  &lt;!ENTITY not    "&amp;#172;"&gt;
  &lt;!ENTITY shy    "&amp;#173;"&gt;
  &lt;!ENTITY reg    "&amp;#174;"&gt;
  &lt;!ENTITY macr   "&amp;#175;"&gt;
  &lt;!ENTITY deg    "&amp;#176;"&gt;
  &lt;!ENTITY plusmn "&amp;#177;"&gt;
  &lt;!ENTITY sup2   "&amp;#178;"&gt;
  &lt;!ENTITY sup3   "&amp;#179;"&gt;
  &lt;!ENTITY acute  "&amp;#180;"&gt;
  &lt;!ENTITY micro  "&amp;#181;"&gt;
  &lt;!ENTITY para   "&amp;#182;"&gt;
  &lt;!ENTITY middot "&amp;#183;"&gt;
  &lt;!ENTITY cedil  "&amp;#184;"&gt;
  &lt;!ENTITY sup1   "&amp;#185;"&gt;
  &lt;!ENTITY ordm   "&amp;#186;"&gt;
  &lt;!ENTITY raquo  "&amp;#187;"&gt;
  &lt;!ENTITY frac14 "&amp;#188;"&gt;
  &lt;!ENTITY frac12 "&amp;#189;"&gt;
  &lt;!ENTITY frac34 "&amp;#190;"&gt;
  &lt;!ENTITY iquest "&amp;#191;"&gt;
  &lt;!ENTITY Agrave "&amp;#192;"&gt;
  &lt;!ENTITY Aacute "&amp;#193;"&gt;
  &lt;!ENTITY Acirc  "&amp;#194;"&gt;
  &lt;!ENTITY Atilde "&amp;#195;"&gt;
  &lt;!ENTITY Auml   "&amp;#196;"&gt;
  &lt;!ENTITY Aring  "&amp;#197;"&gt;
  &lt;!ENTITY AElig  "&amp;#198;"&gt;
  &lt;!ENTITY Ccedil "&amp;#199;"&gt;
  &lt;!ENTITY Egrave "&amp;#200;"&gt;
  &lt;!ENTITY Eacute "&amp;#201;"&gt;
  &lt;!ENTITY Ecirc  "&amp;#202;"&gt;
  &lt;!ENTITY Euml   "&amp;#203;"&gt;
  &lt;!ENTITY Igrave "&amp;#204;"&gt;
  &lt;!ENTITY Iacute "&amp;#205;"&gt;
  &lt;!ENTITY Icirc  "&amp;#206;"&gt;
  &lt;!ENTITY Iuml   "&amp;#207;"&gt;
  &lt;!ENTITY ETH    "&amp;#208;"&gt;
  &lt;!ENTITY Ntilde "&amp;#209;"&gt;
  &lt;!ENTITY Ograve "&amp;#210;"&gt;
  &lt;!ENTITY Oacute "&amp;#211;"&gt;
  &lt;!ENTITY Ocirc  "&amp;#212;"&gt;
  &lt;!ENTITY Otilde "&amp;#213;"&gt;
  &lt;!ENTITY Ouml   "&amp;#214;"&gt;
  &lt;!ENTITY times  "&amp;#215;"&gt;
  &lt;!ENTITY Oslash "&amp;#216;"&gt;
  &lt;!ENTITY Ugrave "&amp;#217;"&gt;
  &lt;!ENTITY Uacute "&amp;#218;"&gt;
  &lt;!ENTITY Ucirc  "&amp;#219;"&gt;
  &lt;!ENTITY Uuml   "&amp;#220;"&gt;
  &lt;!ENTITY Yacute "&amp;#221;"&gt;
  &lt;!ENTITY THORN  "&amp;#222;"&gt;
  &lt;!ENTITY szlig  "&amp;#223;"&gt;
  &lt;!ENTITY agrave "&amp;#224;"&gt;
  &lt;!ENTITY aacute "&amp;#225;"&gt;
  &lt;!ENTITY acirc  "&amp;#226;"&gt;
  &lt;!ENTITY atilde "&amp;#227;"&gt;
  &lt;!ENTITY auml   "&amp;#228;"&gt;
  &lt;!ENTITY aring  "&amp;#229;"&gt;
  &lt;!ENTITY aelig  "&amp;#230;"&gt;
  &lt;!ENTITY ccedil "&amp;#231;"&gt;
  &lt;!ENTITY egrave "&amp;#232;"&gt;
  &lt;!ENTITY eacute "&amp;#233;"&gt;
  &lt;!ENTITY ecirc  "&amp;#234;"&gt;
  &lt;!ENTITY euml   "&amp;#235;"&gt;
  &lt;!ENTITY igrave "&amp;#236;"&gt;
  &lt;!ENTITY iacute "&amp;#237;"&gt;
  &lt;!ENTITY icirc  "&amp;#238;"&gt;
  &lt;!ENTITY iuml   "&amp;#239;"&gt;
  &lt;!ENTITY eth    "&amp;#240;"&gt;
  &lt;!ENTITY ntilde "&amp;#241;"&gt;
  &lt;!ENTITY ograve "&amp;#242;"&gt;
  &lt;!ENTITY oacute "&amp;#243;"&gt;
  &lt;!ENTITY ocirc  "&amp;#244;"&gt;
  &lt;!ENTITY otilde "&amp;#245;"&gt;
  &lt;!ENTITY ouml   "&amp;#246;"&gt;
  &lt;!ENTITY oslash "&amp;#248;"&gt;
  &lt;!ENTITY ugrave "&amp;#249;"&gt;
  &lt;!ENTITY uacute "&amp;#250;"&gt;
  &lt;!ENTITY ucirc  "&amp;#251;"&gt;
  &lt;!ENTITY uuml   "&amp;#252;"&gt;
  &lt;!ENTITY yacute "&amp;#253;"&gt;
  &lt;!ENTITY thorn  "&amp;#254;"&gt;
  &lt;!ENTITY yuml   "&amp;#255;"&gt;
]&gt;
        </xsl:text>
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
      <fo:layout-master-set>
        <fo:simple-page-master master-name="A4-portrait" page-height="29.7cm" page-width="{$page-width}mm" margin="2cm">
          <fo:region-before region-name="before-A4-portrait" extent="1cm"/>
          <fo:region-body margin-top="1.5cm" margin-bottom="1.5cm"/>
          <fo:region-after region-name="after-A4-portrait" extent="1cm"/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      
      <fo:page-sequence master-reference="A4-portrait">
        <fo:static-content flow-name="before-A4-portrait">
          <fo:block font-size="10pt" text-align-last="end">
            <fo:table table-layout="fixed">
              <fo:table-column column-width="396pt"/>
              <fo:table-column column-width="72pt"/>
              <fo:table-body>
                <fo:table-row>
                  <fo:table-cell>
                    <fo:block text-align="start">
                      <xsl:value-of select="$header-left" />
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell>
                    <fo:block text-align="end" font-weight="bold" font-family="monospace">
                      <xsl:value-of select="$header-right" />
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>
              </fo:table-body>
            </fo:table>
          </fo:block>
        </fo:static-content>
        <fo:static-content flow-name="after-A4-portrait">
          <fo:block font-size="10pt" text-align-last="end">
            <fo:table table-layout="fixed">
              <fo:table-column column-width="396pt"/>
              <fo:table-column column-width="72pt"/>
              <fo:table-body>
                <fo:table-row>
                  <fo:table-cell>
                    <fo:block text-align="start">
                      <xsl:value-of select="/html/head/title"/>
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell>
                    <fo:block text-align="end">
                      Page
                      <fo:page-number/>
                      of
                      <fo:page-number-citation ref-id="TheVeryLastPage"/>
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>
              </fo:table-body>
            </fo:table>
          </fo:block>
        </fo:static-content>
        
        <xsl:apply-templates />
      </fo:page-sequence>
    </fo:root>
  </xsl:template>

  <xsl:template match="a[@href != '' and not(starts-with(@href, '#'))]">
    <fo:basic-link color="{$link-colour}" external-destination="{dash.common:Urlify(@href, $root-url)}">
      <xsl:apply-templates />
    </fo:basic-link>
  </xsl:template>

  <xsl:template match="blockquote">
    <fo:block font-size="{1.2 * $default-font-size}pt" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="body">
    <fo:flow flow-name="xsl-region-body">
      <fo:block font-family="sans-serif" font-size="{$default-font-size}pt">
        <xsl:apply-templates />

        <!-- ============================================
                We put an ID at the end of the document so we 
                can do "Page x of y" numbering.
                =============================================== -->
        <fo:block id="TheVeryLastPage" font-size="0pt" line-height="0pt" space-after="0pt"/>
      </fo:block>
    </fo:flow>
  </xsl:template>

  <xsl:template match="cite">
    <fo:block font-size="10pt" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h1">
    <fo:block font-size="{1.4 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h2">
    <fo:block font-size="{1.2 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h3">
    <fo:block font-size="{1.15 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h4">
    <fo:block font-size="{1.1 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h5">
    <fo:block font-size="{1.05 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="h6">
    <fo:block font-size="{1.0 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="iframe[@src != '']">
    <xsl:variable name="border-width">0.2mm</xsl:variable>
    <xsl:variable name="border-colour">#e4e4e4</xsl:variable>
    <xsl:variable name="border-style">solid</xsl:variable>
    <fo:block font-size="{0.8 * $default-font-size}pt" border-left-width="{$border-width}" border-right-width="{$border-width}" border-top-width="{$border-width}" border-bottom-width="{$border-width}" border-left-color="{$border-colour}" border-right-color="{$border-colour}" border-top-color="{$border-colour}" border-bottom-color="{$border-colour}" border-left-style="{$border-style}" border-right-style="{$border-style}" border-top-style="{$border-style}" border-bottom-style="{$border-style}"
              padding-top="2mm" padding-bottom="2mm" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <fo:inline padding-left="2mm" padding-right="2mm">
        <fo:basic-link color="{$link-colour}" external-destination="{@src}">
          <xsl:value-of select="@src" />
        </fo:basic-link>
      </fo:inline>
    </fo:block>
  </xsl:template>

  <xsl:template match="img">
    <xsl:choose>
      <xsl:when test="$include-images">
        <fo:block space-after="2mm">
          <fo:external-graphic src="{dash.common:Urlify(@src, $root-url)}">
            <xsl:if test="@width">
              <xsl:attribute name="width">
                <xsl:choose>
                  <xsl:when test="contains(@width, 'px')">
                    <xsl:value-of select="@width"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat(@width, 'px')"/>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="@height">
              <xsl:attribute name="height">
                <xsl:choose>
                  <xsl:when test="contains(@height, 'px')">
                    <xsl:value-of select="@height"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat(@height, 'px')"/>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:attribute>
            </xsl:if>
          </fo:external-graphic>
        </fo:block>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="border-width">0.2mm</xsl:variable>
        <xsl:variable name="border-colour">#e4e4e4</xsl:variable>
        <xsl:variable name="border-style">solid</xsl:variable>
        <fo:block font-size="{0.8 * $default-font-size}pt" border-left-width="{$border-width}" border-right-width="{$border-width}" border-top-width="{$border-width}" border-bottom-width="{$border-width}" border-left-color="{$border-colour}" border-right-color="{$border-colour}" border-top-color="{$border-colour}" border-bottom-color="{$border-colour}" border-left-style="{$border-style}" border-right-style="{$border-style}" border-top-style="{$border-style}" border-bottom-style="{$border-style}"
                  padding-top="2mm" padding-bottom="2mm" space-before="2mm" space-after="2mm">
          <fo:inline padding-left="2mm" padding-right="2mm">
            <fo:basic-link color="{$link-colour}" external-destination="{dash.common:Urlify(@src, $root-url)}">
              Image:
              <xsl:choose>
                <xsl:when test="@alt != ''">
                  <xsl:value-of select="@alt" />
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="dash.common:Urlify(@src, $root-url)" />
                </xsl:otherwise>
              </xsl:choose>
            </fo:basic-link>
          </fo:inline>
        </fo:block>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="ol|ul">
    <xsl:variable name="vertical-spacing-mm">
      <xsl:choose>
        <xsl:when test="ancestor::ul or ancestor::ol">
          <xsl:text>0</xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$default-vertical-spacing-mm" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <fo:list-block provisional-distance-between-starts="{$default-list-label-spacing-mm}mm" provisional-label-separation="0mm" space-before="{$vertical-spacing-mm}mm" space-after="{$vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:list-block>
  </xsl:template>

  <!-- ============================================
    When we handle items in an ordered list, we need
    to check if the list has a start attribute.  If
    it does, we change the starting number accordingly.
    Once we've figured out where to start counting,
    we check the type attribute to see what format
    the numbers should use.  
    =============================================== -->

  <xsl:template match="ol/li">
    <fo:list-item>
      <fo:list-item-label end-indent="label-end()">
        <fo:block>
          <xsl:variable name="value-attr">
            <xsl:choose>
              <xsl:when test="../@start">
                <xsl:number value="position() + ../@start - 1"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:number value="position()"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:choose>
            <xsl:when test="../@type='i'">
              <xsl:number value="$value-attr" format="i. "/>
            </xsl:when>
            <xsl:when test="../@type='I'">
              <xsl:number value="$value-attr" format="I. "/>
            </xsl:when>
            <xsl:when test="../@type='a'">
              <xsl:number value="$value-attr" format="a. "/>
            </xsl:when>
            <xsl:when test="../@type='A'">
              <xsl:number value="$value-attr" format="A. "/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:number value="$value-attr" format="1. "/>
            </xsl:otherwise>
          </xsl:choose>
        </fo:block>
      </fo:list-item-label>
      <fo:list-item-body start-indent="body-start()">
        <fo:block>
          <xsl:apply-templates />
        </fo:block>
      </fo:list-item-body>
    </fo:list-item>
  </xsl:template>

  <xsl:template match="p">
    <fo:block space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="strong">
    <fo:inline font-weight="bold">
      <xsl:apply-templates select="*|text()"/>
    </fo:inline>
  </xsl:template>

  <xsl:template match="sub">
    <fo:inline vertical-align="sub" font-size="75%">
      <xsl:apply-templates />
    </fo:inline>
  </xsl:template>

  <xsl:template match="sup">
    <fo:inline vertical-align="super" font-size="75%">
      <xsl:apply-templates />
    </fo:inline>
  </xsl:template>

  <!-- ============================================
    Tables are a hassle.  The main problem we have
    is converting the cols attribute into some 
    number of <fo:table-column> elements.  We do 
    this with a named template called build-columns.
    Once we've processed the cols attribute, we 
    invoke all of the templates for the children 
    of this element. 
    =============================================== -->
  <xsl:template match="table">
    <fo:table table-layout="fixed" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:choose>
        <xsl:when test="@cols">
          <xsl:call-template name="build-columns">
            <xsl:with-param name="cols" select="concat(@cols, ' ')" />
          </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
          <xsl:for-each select="tr[1]/th|tr[1]/td">
            <fo:table-column />
          </xsl:for-each>
        </xsl:otherwise>
      </xsl:choose>
      <fo:table-body>
        <xsl:apply-templates />
      </fo:table-body>
    </fo:table>
  </xsl:template>

  <!-- ============================================
    For a table cell, we put everything inside a
    <fo:table-cell> element.  We set the padding
    property correctly, then we set the border 
    style.  For the border style, we look to see if
    any of the ancestor elements we care about 
    specified a solid border.  Next, we check for the 
    rowspan, colspan, and align attributes.  Notice 
    that for align, we check this element, then go
    up the ancestor chain until we find the <table>
    element or we find something that specifies the 
    alignment. 
    =============================================== -->
  <xsl:template match="td">
    <fo:table-cell
      padding-start="3pt" padding-end="3pt"
      padding-before="3pt" padding-after="3pt">
      <xsl:if test="@colspan">
        <xsl:attribute name="number-columns-spanned">
          <xsl:value-of select="@colspan"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@rowspan">
        <xsl:attribute name="number-rows-spanned">
          <xsl:value-of select="@rowspan"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@border='1' or 
                    ancestor::tr[@border='1'] or
                    ancestor::thead[@border='1'] or
                    ancestor::table[@border='1']">
        <xsl:attribute name="border-style">
          <xsl:text>solid</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="border-color">
          <xsl:text>black</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="border-width">
          <xsl:text>1pt</xsl:text>
        </xsl:attribute>
      </xsl:if>
      <xsl:variable name="align">
        <xsl:choose>
          <xsl:when test="@align">
            <xsl:choose>
              <xsl:when test="@align='center'">
                <xsl:text>center</xsl:text>
              </xsl:when>
              <xsl:when test="@align='right'">
                <xsl:text>end</xsl:text>
              </xsl:when>
              <xsl:when test="@align='justify'">
                <xsl:text>justify</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>start</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:when test="ancestor::tr[@align]">
            <xsl:choose>
              <xsl:when test="ancestor::tr/@align='center'">
                <xsl:text>center</xsl:text>
              </xsl:when>
              <xsl:when test="ancestor::tr/@align='right'">
                <xsl:text>end</xsl:text>
              </xsl:when>
              <xsl:when test="ancestor::tr/@align='justify'">
                <xsl:text>justify</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>start</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:when test="ancestor::thead">
            <xsl:text>center</xsl:text>
          </xsl:when>
          <xsl:when test="ancestor::table[@align]">
            <xsl:choose>
              <xsl:when test="ancestor::table/@align='center'">
                <xsl:text>center</xsl:text>
              </xsl:when>
              <xsl:when test="ancestor::table/@align='right'">
                <xsl:text>end</xsl:text>
              </xsl:when>
              <xsl:when test="ancestor::table/@align='justify'">
                <xsl:text>justify</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>start</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:otherwise>
            <xsl:text>start</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:variable>
      <fo:block text-align="{$align}">
        <xsl:apply-templates />
      </fo:block>
    </fo:table-cell>
  </xsl:template>

  <!-- ============================================
    The rarely-used <tfoot> element contains some
    number of <tr> elements; we just invoke the 
    template for <tr> here. 
    =============================================== -->
  <xsl:template match="tfoot">
    <xsl:apply-templates select="tr"/>
  </xsl:template>

  <!-- ============================================
    If there's a <th> element, we process it just 
    like a normal <td>, except the font-weight is 
    always bold and the text-align is always center. 
    =============================================== -->
  <xsl:template match="th">
    <fo:table-cell
      padding-start="3pt" padding-end="3pt"
      padding-before="3pt" padding-after="3pt">
      <xsl:if test="@border='1' or 
                    ancestor::tr[@border='1'] or
                    ancestor::table[@border='1']">
        <xsl:attribute name="border-style">
          <xsl:text>solid</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="border-color">
          <xsl:text>black</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="border-width">
          <xsl:text>1pt</xsl:text>
        </xsl:attribute>
      </xsl:if>
      <fo:block font-weight="bold" text-align="center">
        <xsl:apply-templates />
      </fo:block>
    </fo:table-cell>
  </xsl:template>

  <!-- ============================================
    Just like <tfoot>, the rarely-used <thead> element
    contains some number of table rows.  We just 
    invoke the template for <tr> here. 
    =============================================== -->
  <xsl:template match="thead">
    <xsl:apply-templates select="tr"/>
  </xsl:template>

  <!-- ============================================
    For an HTML table row, we create an XSL-FO table
    row, then invoke the templates for everything 
    inside it. 
    =============================================== -->
  <xsl:template match="tr">
    <fo:table-row>
      <xsl:apply-templates />
    </fo:table-row>
  </xsl:template>

  <xsl:template match="ul/li">
    <fo:list-item>
      <fo:list-item-label end-indent="label-end()">
        <!--<fo:block>&#x2022;</fo:block>-->
        <fo:block>-</fo:block>
      </fo:list-item-label>
      <fo:list-item-body start-indent="body-start()">
        <fo:block>
          <xsl:apply-templates />
        </fo:block>
      </fo:list-item-body>
    </fo:list-item>
  </xsl:template>

  <!-- ============================================
    This template generates an <fo:table-column>
    element for each token in the cols attribute of
    the HTML <table> tag.  The template processes
    the first token, then invokes itself with the 
    rest of the string. 
    =============================================== -->
  <xsl:template name="build-columns">
    <xsl:param name="cols"/>

    <xsl:if test="string-length(normalize-space($cols))">
      <xsl:variable name="next-col">
        <xsl:value-of select="substring-before($cols, ' ')"/>
      </xsl:variable>
      <xsl:variable name="remaining-cols">
        <xsl:value-of select="substring-after($cols, ' ')"/>
      </xsl:variable>
      <xsl:choose>
        <xsl:when test="contains($next-col, 'pt')">
          <fo:table-column column-width="{$next-col}"/>
        </xsl:when>
        <xsl:when test="number($next-col) &gt; 0">
          <fo:table-column column-width="{concat($next-col, 'pt')}"/>
        </xsl:when>
        <xsl:otherwise>
          <fo:table-column column-width="50pt"/>
        </xsl:otherwise>
      </xsl:choose>

      <xsl:call-template name="build-columns">
        <xsl:with-param name="cols" select="concat($remaining-cols, ' ')"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
