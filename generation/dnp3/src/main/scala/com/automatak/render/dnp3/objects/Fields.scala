package com.automatak.render.dnp3.objects

import com.automatak.render.EnumModel
import com.automatak.render.dnp3.enums.{IntervalUnit, ControlCode, CommandStatus}

sealed trait FieldType
class FixedSizeFieldType(val numBytes: Int) extends FieldType

case object UInt8Field extends FixedSizeFieldType(1)
case object UInt16Field extends FixedSizeFieldType(2)
case object UInt32Field extends FixedSizeFieldType(4)
case object UInt48Field extends FixedSizeFieldType(6)
case object SInt16Field extends FixedSizeFieldType(2)
case object SInt32Field extends FixedSizeFieldType(4)
case object Float32Field extends FixedSizeFieldType(4)
case object Float64Field extends FixedSizeFieldType(8)
case class EnumField(model: EnumModel) extends FixedSizeFieldType(1)

object FixedSizeField {

  //common flags field
  val flags = FixedSizeField("flags", UInt8Field)


  // SA stuff
  val csq = FixedSizeField("challengeSeqNum", UInt32Field)
  val ksq = FixedSizeField("keyChangeSeqNum", UInt32Field)
  val user = FixedSizeField("userNum", UInt16Field)
  val assocId = FixedSizeField("assocId", UInt16Field)
  val macAlgo = FixedSizeField("macAlgo", UInt8Field)
  val keyWrapAlgo = FixedSizeField("keyWrapAlgo", UInt8Field)
  val keyStatus = FixedSizeField("keyStatus", UInt8Field)
  val challengeReason = FixedSizeField("challengeReason", UInt8Field)
  val errorCode = FixedSizeField("errorCode", UInt8Field)
  val keyChangeMethod = FixedSizeField("keyChangeMethod", UInt8Field)
  val certificateType = FixedSizeField("certificateType", UInt8Field)


  // timestamps
  val time16 = FixedSizeField("time", UInt16Field)
  val time48 = FixedSizeField("time", UInt48Field)

  // counter values
  val count16 = FixedSizeField("value", UInt16Field)
  val count32 = FixedSizeField("value", UInt32Field)

  // analog values
  val value16 = FixedSizeField("value", SInt16Field)
  val value32 = FixedSizeField("value", SInt32Field)
  val float32 = FixedSizeField("value", Float32Field)
  val float64 = FixedSizeField("value", Float64Field)

  //enums
  val commandStatus = FixedSizeField("status", UInt8Field)
  val intervalUnit = FixedSizeField("intervalUnit", EnumField(IntervalUnit()))

}

object VariableFields {
  val challengeData = VariableField("challengeData", Some(4))
  val hmac = VariableField("hmac", Some(4))
  val keyWrapData = VariableField("keyWrapData", None)
  val errorText = VariableField("errorText", None)
  val certificate = VariableField("certificate", None)
}

case class FixedSizeField(name: String, typ: FixedSizeFieldType)

case class VariableField(name: String, minLength: Option[Int])


