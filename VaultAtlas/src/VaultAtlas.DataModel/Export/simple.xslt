<?xml version="1.0" encoding="utf-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"><xsl:template match="/">
  <html><head>
  <style><xsl:value-of select="/VaultAtlas/Settings[Name='Stylesheet']/Value"></xsl:value-of></style>
  <xsl:variable name="linkbase" select="/VaultAtlas/Settings[Name='Linkbase']/Value"></xsl:variable>
  <BASE HREF="{$linkbase}"></BASE>
  </head>
  <body>
  	<xsl:apply-templates />
	</body></html>
</xsl:template>

<xsl:template match="VaultAtlas">
<h2><xsl:value-of select="@ListName"/></h2>
    <table CLASS="" border="0" cellpadding="2" cellspacing="0" width="100%">
      <tr>
        <xsl:for-each select="Shows">
        	<TR>
        	<TD><xsl:value-of select="Artist" /></TD>
        	<TD><xsl:value-of select="Date" /></TD>
        	<TD><xsl:value-of select="City" /></TD>
        	<TD><xsl:value-of select="Venue" /></TD>
        	<TD><xsl:value-of select="Length" /></TD>
        	<TD><xsl:value-of select="Source" /></TD>
        	</TR>
        </xsl:for-each>
      </tr>
    </table>
</xsl:template>
</xsl:transform>