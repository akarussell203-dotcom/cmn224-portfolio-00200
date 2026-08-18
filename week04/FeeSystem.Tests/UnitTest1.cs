using System.Collections.Generic;
using System;
using System.Linq;


namespace FeeSystem.Tests;

public class FeeCalculatorTests
{
    //Case 1
    [Fact]
    public void OutstandingBalance_NoPayment_ReturnsFullFee()
    {
        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>();

        //Act
        var results = calc.OutstandingBalance(600m, payments);

        //Assert
        Assert.Equal(600m, results);
    }



    //Case 2
    [Fact]
    public void OutstandingBalance_PartialPayment_ReturnsRemainingFee()
    {
        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m };//200 paid

        //Act
        var results = calc.OutstandingBalance(600m, payments);//600 full

        //Assert
        Assert.Equal(400m, results);//400 remaining
    }



    //Case 3
    [Fact]
    public void OutstandingBalance_SeveralInstallments_ReturnRemainingFee()
    {

        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m, 200m, 100m }; //List of installments paid

        //Act
        var results = calc.OutstandingBalance(600m, payments);//600 FullFee

        //Assert
        Assert.Equal(100m, results); //100m outstanding
    }



    //Case 4
    [Fact]
    public void OutstandingBalance_FeeFullyPaid_ReturnNothing()
    {

        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 600m }; //600 Fully paid

        //Act
        var results = calc.OutstandingBalance(600m, payments);//600 FullFee

        //Assert
        Assert.Equal(0m, results); //0m outstanding
    }



    //Case 5
    [Fact]
    public void OutstandingBalance_Overpeyment_ReturnOverPaidAmount()
    {

        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 700m }; //600m FullFee + 100m

        //Act
        var results = calc.OutstandingBalance(600m, payments);//600 FullFee

        //Assert
        Assert.Equal(-100m, results); //100m Overpeyment
    }



    //Case 6
    [Fact]
    public void OutstandingBalance_NegativeFee_ThrowsArgumentException()
    {
        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>(); //nullPaymeny

        //Act and Assert.
        Assert.Throws<ArgumentException>(() => calc.OutstandingBalance(-600m, payments)); //Throw ArgumentException if termFee<0
    }



    //Case 7
    [Fact]
    public void OutstandingBalance_HalfPayment_ReturnTrue()
    {

        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 300m }; //300m Exactly Half

        //Act
        var result = calc.IsClearedForExams(600m, payments);//600 FullFee

        //Assert
        Assert.True(result); //100m Outstanding
    }



    //Case 8
    [Fact]
    public void OutstandingBalance_BelowHalfPayment_ReturnFalse()
    {

        //Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 299.99m }; //one Toea Below Half

        //Act
        var results = calc.IsClearedForExams(600m, payments);//600 FullFee

        //Assert
        Assert.False(results); //100m Overpeyment
    }
}
