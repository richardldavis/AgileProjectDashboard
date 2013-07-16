<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fo="http://www.w3.org/1999/XSL/Format"
    xmlns:exsl="http://exslt.org/common"
    xmlns:regexp="http://exslt.org/regular-expressions"
    extension-element-prefixes="exsl regexp">
  <xsl:import href="xslfo-common.xslt" />

  <xsl:template match="span[@class='status']">
    <fo:inline font-style="italic">
      <xsl:apply-templates />
    </fo:inline>
  </xsl:template>

  <xsl:template match="p[@class='attributes']">
    <fo:block font-size="{0.8 * $default-font-size}pt" color="{$lowlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm">
      <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="ul[@class = 'stories']/li/h3">
    <fo:block font-size="{1.3 * $default-font-size}pt" color="{$highlight-colour}" space-before="{$default-vertical-spacing-mm}mm" space-after="{$default-vertical-spacing-mm}mm"
              border-bottom-width="1px" border-bottom-style="solid" border-bottom-color="{$highlight-colour}" padding-bottom="0.5mm">
        <xsl:apply-templates />
    </fo:block>
  </xsl:template>

  <xsl:template match="ul[@class = 'tags']/li">
    <fo:list-item>
      <fo:list-item-label end-indent="label-end()">
        <fo:block></fo:block>
      </fo:list-item-label>
      <fo:list-item-body start-indent="body-start()">
        <fo:block>
          <xsl:apply-templates />
        </fo:block>
      </fo:list-item-body>
    </fo:list-item>
  </xsl:template>

  <xsl:template match="ul[@class = 'stories']/li">
    <fo:list-item space-after="{$default-vertical-spacing-mm * 5}mm">
      <fo:list-item-label end-indent="label-end()">
        <fo:block></fo:block>
      </fo:list-item-label>
      <fo:list-item-body start-indent="body-start()">
        <fo:block>
          <xsl:apply-templates />
        </fo:block>
      </fo:list-item-body>
    </fo:list-item>
  </xsl:template>

</xsl:stylesheet>
