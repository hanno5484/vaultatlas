<?xml version="1.0" encoding="utf-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html><head>
  <style><xsl:value-of select="/VaultAtlas/Settings[Name='Stylesheet']/Value"></xsl:value-of></style>
  <xsl:variable name="linkbase" select="/VaultAtlas/Settings[Name='Linkbase']/Value"></xsl:variable>
  <BASE HREF="{$linkbase}"></BASE>
  </head>
  
  <body>

<h2><xsl:value-of select="@ListName"/></h2>
    <table border="0" cellpadding="2" cellspacing="0">

<xsl:for-each select="/VaultAtlas/IncludedArtists">

<xsl:variable name="Artistname" select="." />

<TR><TD COLSPAN="5" CLASS="ArtistTitleCell"><SPAN CLASS="ArtistTitle"><xsl:value-of select="$Artistname" /></SPAN> <SPAN CLASS="NumberOfShows">(<xsl:value-of select="count(/VaultAtlas/Shows[Artist= $Artistname])"/> entries)</SPAN></TD></TR>

<xsl:for-each select="/VaultAtlas/Shows[Artist= $Artistname]">

       	<TR>
       	<TD width="100"><xsl:value-of select="DateDisplay" /></TD>
       	<TD><xsl:value-of select="City" /></TD>
       	<TD><xsl:value-of select="Venue" /></TD>
       	<TD><xsl:value-of select="Length" /></TD>
       	<TD><xsl:value-of select="Source" /></TD>
       	</TR>

      <xsl:if test="Comments">
      	<tr>
      	<td></td>
      	<td colspan="4" class="Comments"><xsl:value-of select="Comments" /></td>
      	</tr>
      </xsl:if>
 </xsl:for-each> 
 </xsl:for-each>
 
    </table>
    	</body></html>

</xsl:template>
</xsl:transform>