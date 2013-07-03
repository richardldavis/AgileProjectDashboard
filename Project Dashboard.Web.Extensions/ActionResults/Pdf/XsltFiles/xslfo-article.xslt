<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fo="http://www.w3.org/1999/XSL/Format"
    xmlns:exsl="http://exslt.org/common"
    xmlns:regexp="http://exslt.org/regular-expressions"
    extension-element-prefixes="exsl regexp">
    <xsl:import href="xslfo-common.xslt" />
    
    <xsl:template match="article">
        <fo:block background-color="white" padding-left="2mm" padding-right="2mm" padding-top="2mm" padding-bottom="2mm">
            <xsl:apply-templates />
        </fo:block>
    </xsl:template>

    <xsl:template match="h1">
        <fo:block font-size="{1.4 * $default-font-size}pt" space-before="5mm" space-after="5mm">
            <fo:inline background-color="{$highlight-colour}" color="white" padding-left="2mm" padding-right="2mm" padding-top="2mm" padding-bottom="2mm">
                <xsl:apply-templates />
            </fo:inline>
        </fo:block>
    </xsl:template>

    <xsl:template match="h2">
        <fo:block font-size="{1.2 * $default-font-size}pt" color="{$highlight-colour}" space-before="5mm" space-after="5mm">
            <xsl:apply-templates />
        </fo:block>
    </xsl:template>

    <xsl:template match="p[@class='intro']">
        <fo:block font-size="{1.2 * $default-font-size}" color="{$highlight-colour}" background-color="white" padding-left="2mm" padding-right="2mm">
            <fo:block border-bottom-width="1px" border-bottom-style="solid" border-bottom-color="#cccccc" padding-top="2mm" padding-bottom="2mm">
                <xsl:apply-templates />
            </fo:block>
        </fo:block>
    </xsl:template>

    <xsl:template match="div[@id='main']">
        <xsl:variable name="image-url">
            <xsl:variable name="regex">background-image:(url\(\')?(.*?)(\'\))?;</xsl:variable>
            <xsl:value-of select="regexp:match(@style, $regex, 'i')[position() = 3]" />
        </xsl:variable>
        <fo:block background-repeat="no-repeat" background-color="#e6e6e6" padding-left="5mm" padding-right="5mm" padding-top="5mm" padding-bottom="5mm">
            <xsl:if test="$image-url != '' and $include-images">
                <xsl:attribute name="background-image">
                    <xsl:value-of select="concat($root-url,$image-url)"/>
                </xsl:attribute>
            </xsl:if>
            <xsl:apply-templates />
        </fo:block>
    </xsl:template>

</xsl:stylesheet>
